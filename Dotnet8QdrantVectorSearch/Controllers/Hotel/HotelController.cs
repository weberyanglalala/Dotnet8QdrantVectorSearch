using System.Diagnostics.CodeAnalysis;
using Dotnet8QdrantVectorSearch.Controllers.Hotel.Dtos;
using Dotnet8QdrantVectorSearch.Models;
using Dotnet8QdrantVectorSearch.Services.Product;
using Dotnet8QdrantVectorSearch.Services.Qdrant;
using Dotnet8QdrantVectorSearch.Services.Qdrant.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8QdrantVectorSearch.Controllers.Hotel;

[Route("api/[controller]/[action]")]
[ApiController]
[Experimental("SKEXP0001")]
public class HotelController : ControllerBase
{
    private readonly QdrantService _qdrantService;
    private readonly ProductChatService _productChatService;

    public HotelController(QdrantService qdrantService, ProductChatService productChatService)
    {
        _qdrantService = qdrantService;
        _productChatService = productChatService;
    }

    /// <summary>
    /// 初始化並填充Qdrant向量數據庫中的酒店資料
    /// </summary>
    /// <remarks>
    /// 此端點將創建30個範例酒店記錄，包括酒店ID、名稱和描述，
    /// 並為每個酒店的描述生成嵌入向量，然後將這些記錄存儲在Qdrant集合中。
    /// 此操作為後續的向量搜索提供基礎數據。
    /// </remarks>
    /// <returns>成功時返回包含True的API響應</returns>
    /// <response code="200">數據填充成功</response>
    public async Task<IActionResult> SeedDataAsync()
    {
        await _qdrantService.SeedFakeHotelCollectionAsync();
        return Ok(ApiResponse<bool>.Success(true));
    }

    /// <summary>
    /// 在Qdrant中創建用於存儲酒店數據的向量集合
    /// </summary>
    /// <remarks>
    /// 此端點檢查並確保Qdrant中存在名為"skhotels"的向量集合。
    /// 如果集合不存在，則創建一個新集合。
    /// 此操作應在填充數據或執行搜索之前完成。
    /// </remarks>
    /// <returns>成功時返回包含True的API響應</returns>
    /// <response code="200">集合創建成功或已存在</response>
    public async Task<IActionResult> CreateCollectionAsync()
    {
        await _qdrantService.CreateCollectionIfNotExistsAsync();
        return Ok(ApiResponse<bool>.Success(true));
    }

    /// <summary>
    /// 根據搜索詞對酒店進行語義向量搜索
    /// </summary>
    /// <remarks>
    /// 此端點將用戶提供的搜索詞轉換為嵌入向量，
    /// 然後在Qdrant向量數據庫中執行相似度搜索，
    /// 返回與搜索詞在語義上最相似的前10家酒店。
    /// 結果按相似度分數排序。
    /// </remarks>
    /// <param name="searchTerm">要搜索的文本詞語，如"海灘"或"市中心"</param>
    /// <param name="top"></param>
    /// <returns>包含最多10個匹配酒店的API響應</returns>
    /// <response code="200">搜索成功，返回匹配的酒店列表</response>
    public async Task<IActionResult> GenerateEmbeddingAndVectorSearchAsync([FromQuery] string searchTerm, [FromQuery] int? top = 10)
    {
        var result = await _qdrantService.GenerateEmbeddingsAndVectorSearchAsync(searchTerm, top ?? 10);
        return Ok(ApiResponse<List<HotelSearchResult>>.Success(result));
    }
    
    /// <summary>
    /// 根據搜索詞和關鍵詞對酒店進行混合向量搜索
    /// </summary>
    /// <remarks>
    /// 此端點將用戶提供的搜索詞轉換為嵌入向量，並結合給定的關鍵詞，
    /// 在Qdrant向量數據庫中執行混合相似度搜索，
    /// 返回與搜索詞在語義上最相似且符合關鍵詞條件的前10家酒店。
    /// 結果按相似度分數排序。
    /// </remarks>
    /// <param name="request">包含搜索詞和關鍵詞的請求對象</param>
    /// <returns>包含最多10個匹配酒店的API響應</returns>
    /// <response code="200">搜索成功，返回匹配的酒店列表</response>
    [HttpGet]
    public async Task<IActionResult> GenerateEmbeddingAndHybridSearchAsync([FromQuery] GenerateEmbeddingAndHybridSearchRequest request)
    {
        var result = await _qdrantService.GenerateEmbeddingsAndHybridSearchAsync(request.SearchTerm, request.Keywords);
        return Ok(ApiResponse<List<HotelSearchResult>>.Success(result));
    }

    
    /// <summary>
    /// 提供旅館電商專業客服的聊天回應服務
    /// </summary>
    /// <remarks>
    /// 此端點利用 Semantic Kernel 技術與 OpenAI 的聊天完成服務，
    /// 維護對話上下文並扮演專業旅館電商客服角色，
    /// 能針對使用者的旅館相關問題提供準確回應。
    /// 系統會保存聊天歷史以實現連續對話功能，並可自動調用 Kernel 函數增強回應能力。
    /// </remarks>
    /// <param name="text">使用者輸入的文本查詢</param>
    /// <returns>包含旅館客服 AI 回應的 API 響應</returns>
    /// <response code="200">查詢處理成功，返回對話回應</response>
    [HttpGet]
    public async Task<IActionResult> SemanticKernelChatAsync([FromQuery] string text)
    {
        var result = await _productChatService.GetResponse(text);
        return Ok(ApiResponse<string>.Success(result));
    }
}