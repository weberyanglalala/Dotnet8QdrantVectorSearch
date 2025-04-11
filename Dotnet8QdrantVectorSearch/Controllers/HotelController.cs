using System.Diagnostics.CodeAnalysis;
using Dotnet8QdrantVectorSearch.Models;
using Dotnet8QdrantVectorSearch.Services.Qdrant;
using Dotnet8QdrantVectorSearch.Services.Qdrant.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8QdrantVectorSearch.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Experimental("SKEXP0001")]
public class HotelController : ControllerBase
{
    private readonly QdrantService _qdrantService;

    public HotelController(QdrantService qdrantService)
    {
        _qdrantService = qdrantService;
    }

    public async Task<IActionResult> SeedDataAsync()
    {
        await _qdrantService.SeedFakeHotelCollectionAsync();
        return Ok(ApiResponse<bool>.Success(true));
    }

    public async Task<IActionResult> CreateCollectionAsync()
    {
        await _qdrantService.CreateCollectionIfNotExistsAsync();
        return Ok(ApiResponse<bool>.Success(true));
    }

    public async Task<IActionResult> GenerateEmbeddingAndSearchAsync(string searchTerm)
    {
        var result = await _qdrantService.GenerateEmbeddingsAndSearchAsync(searchTerm);
        return Ok(ApiResponse<List<HotelSearchResult>>.Success(result));
    }
}