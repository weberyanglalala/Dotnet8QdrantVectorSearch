using System.ComponentModel.DataAnnotations;

namespace Dotnet8QdrantVectorSearch.Controllers.Hotel.Dtos;

public class GenerateEmbeddingAndHybridSearchRequest
{
    [Required]
    public string SearchTerm { get; set; }
    [Required]
    public string[] Keywords { get; set; }
}