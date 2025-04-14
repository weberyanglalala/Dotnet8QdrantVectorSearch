using System.Diagnostics.CodeAnalysis;
using Dotnet8QdrantVectorSearch.Models;
using Dotnet8QdrantVectorSearch.Services.Product;
using Dotnet8QdrantVectorSearch.Services.Qdrant;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;
using Exception = System.Exception;

namespace Dotnet8QdrantVectorSearch;

public class Program
{
    [Experimental("SKEXP0010")]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddDbContext<AvidaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddTransient<ProductServiceByEfCore>();
        builder.Services.AddTransient<ProductEFCorePlugin>();
        builder.Services.AddTransient<ProductChatService>(sp =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            var apiKey = configuration["OpenAiApiKey"]
                ?? throw new Exception("OpenAiApiKey is not configured.");
            kernelBuilder.Services.AddOpenAIChatCompletion("gpt-4o-mini", apiKey);
            var productEfCorePlugin = sp.GetRequiredService<ProductEFCorePlugin>();
            kernelBuilder.Plugins.AddFromObject(productEfCorePlugin);
            var kernel = kernelBuilder.Build();
            return new ProductChatService(kernel);
        });
        
        builder.Services.AddSingleton<QdrantClient>(sp => new QdrantClient(
            host: configuration["Qdrant:Host"] ?? throw new InvalidOperationException("Qdrant:Host is not configured."),
            https: true,
            apiKey: configuration["Qdrant:ApiKey"] ??
                    throw new InvalidOperationException("Qdrant:ApiKey is not configured.")
        ));

        builder.Services.AddScoped<QdrantVectorStore>(sp =>
        {
            var client = sp.GetRequiredService<QdrantClient>();
            var vectorStore = new QdrantVectorStore(client);
            return vectorStore;
        });


        builder.Services.AddScoped<QdrantService>(sp =>
        {
            var client = sp.GetRequiredService<QdrantClient>();
            var kernel = sp.GetRequiredService<Kernel>();
            var logger = sp.GetRequiredService<ILogger<QdrantService>>();
            var vectorStore = sp.GetRequiredService<QdrantVectorStore>();
            return new QdrantService(client, vectorStore, kernel, logger);
        });

        builder.Services.AddSingleton<Kernel>(sp =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            var googleGeminiApiKey = configuration["GoogleGeminiApiKey"]
                                     ?? throw new InvalidOperationException("GoogleGeminiApiKey is not configured.");
            var openAiApiKey = configuration["OpenAiApiKey"]
                               ?? throw new InvalidOperationException("OpenAiApiKey is not configured.");
            kernelBuilder.Services.AddGoogleAIGeminiChatCompletion("gemini-2.0-flash", googleGeminiApiKey);
            // text-embedding-3-small is a 1536-dimension embedding model
            kernelBuilder.Services.AddOpenAITextEmbeddingGeneration("text-embedding-3-small", openAiApiKey);
            // gemini-embedding-exp-03-07 is a 3072-dimension embedding model
            // kernelBuilder.Services.AddGoogleAIEmbeddingGeneration("gemini-embedding-exp-03-07", googleGeminiApiKey);
            return kernelBuilder.Build();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}