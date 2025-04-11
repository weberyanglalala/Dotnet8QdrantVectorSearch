using System.Diagnostics.CodeAnalysis;
using Dotnet8QdrantVectorSearch.Services;
using Microsoft.SemanticKernel;
using Qdrant.Client;

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
        builder.Services.AddSingleton<QdrantClient>(sp => new QdrantClient(
            host: configuration["Qdrant:Host"] ?? throw new InvalidOperationException("Qdrant:Host is not configured."),
            https: true,
            apiKey: configuration["Qdrant:ApiKey"] ??
                    throw new InvalidOperationException("Qdrant:ApiKey is not configured.")
        ));
        

        builder.Services.AddScoped<QdrantService>(sp =>
        {
            var client = sp.GetRequiredService<QdrantClient>();
            var kernel = sp.GetRequiredService<Kernel>();
            return new QdrantService(client, kernel);
        });

        builder.Services.AddSingleton<Kernel>(sp =>
        {
            var kernelBuilder = Kernel.CreateBuilder();

            var openAiApiKey = configuration["OpenAiApiKey"]
                               ?? throw new InvalidOperationException("OpenAiApiKey is not configured.");
            kernelBuilder.Services.AddOpenAIChatCompletion("gpt-4o-mini", openAiApiKey);
            kernelBuilder.Services.AddOpenAITextEmbeddingGeneration("text-embedding-3-small", openAiApiKey);
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