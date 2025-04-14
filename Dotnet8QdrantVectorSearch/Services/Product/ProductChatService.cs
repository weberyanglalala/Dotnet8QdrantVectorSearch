using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Dotnet8QdrantVectorSearch.Services.Product;

public class ProductChatService
{
    private readonly Kernel _kernel;
    private readonly ChatHistory _chatHistory;
    private readonly IChatCompletionService _chatCompletionService;

    private const string PROMPT = """
                                  你是一個專業的旅館電商客服人員，請根據用戶的問題提供準確的答案。
                                  """;

    public ProductChatService(Kernel kernel)
    {
        _kernel = kernel;
        _chatHistory = new ChatHistory();
        _chatHistory.AddSystemMessage(PROMPT);
        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }
    
    public async Task<string> GetResponse(string input)
    {
        _chatHistory.AddUserMessage(input);
        var executionSettings = new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };
        var result = await _chatCompletionService.GetChatMessageContentAsync(_chatHistory, executionSettings, _kernel);
        return result.Content;
    }
}