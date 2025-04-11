namespace Dotnet8QdrantVectorSearch.Services.Qdrant.Dtos;

public class HotelSearchResult
{
    public double Score { get; set; }
    public ulong HotelId { get; set; }
    public string HotelName { get; set; }
    public string Description { get; set; }
}