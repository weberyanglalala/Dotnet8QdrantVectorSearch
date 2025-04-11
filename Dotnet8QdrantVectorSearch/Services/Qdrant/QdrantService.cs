using System.Diagnostics.CodeAnalysis;
using Dotnet8QdrantVectorSearch.Models;
using Dotnet8QdrantVectorSearch.Services.Qdrant.Dtos;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Embeddings;
using Qdrant.Client;

namespace Dotnet8QdrantVectorSearch.Services.Qdrant;

[Experimental("SKEXP0001")]
public class QdrantService
{
    private readonly QdrantClient _client;
    private readonly QdrantVectorStore _vectorStore;
    private readonly IVectorStoreRecordCollection<ulong, Hotel> _hotelCollection;
    private readonly IKeywordHybridSearch<Hotel> _hotelKeywordHybridSearch;
    private readonly Kernel _kernel;
    private readonly ITextEmbeddingGenerationService _embeddingGenerationService;
    private readonly ILogger<QdrantService> _logger;

    public QdrantService(QdrantClient client, Kernel kernel, ILogger<QdrantService> logger)
    {
        _client = client;
        _vectorStore = new QdrantVectorStore(_client);
        _hotelCollection = _vectorStore.GetCollection<ulong, Hotel>("skhotels");
        _kernel = kernel;
        _logger = logger;
        _embeddingGenerationService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
        IVectorStore vectorStore = new QdrantVectorStore(_client);
        _hotelKeywordHybridSearch = (IKeywordHybridSearch<Hotel>)vectorStore.GetCollection<ulong, Hotel>("skhotels");
    }

    public async Task<ReadOnlyMemory<float>> GenerateEmbeddingAsync(string text)
    {
        return await _embeddingGenerationService.GenerateEmbeddingAsync(text);
    }

    public async Task CreateCollectionIfNotExistsAsync()
    {
        await _hotelCollection.CreateCollectionIfNotExistsAsync();
    }

    public async Task SeedFakeHotelCollectionAsync()
    {
        var hotels = new List<Hotel>
        {
            new Hotel
            {
                HotelId = 1,
                HotelName = "大皇宮酒店",
                Description = "位於市中心的一家豪華酒店，提供壯麗景色和世界級設施。享受寬敞的客房、美食餐廳和屋頂泳池。適合商務和休閒旅客。"
            },
            new Hotel
            {
                HotelId = 2,
                HotelName = "日落大道度假村",
                Description = "坐落在海灘旁，這家度假村提供寧靜的避風港，享有美麗的日落和一流服務。賓客可享受水上運動、spa護理和高級餐飲。適合浪漫之旅。"
            },
            new Hotel
            {
                HotelId = 3,
                HotelName = "山景旅館",
                Description = "一座被壯觀山脈環繞的溫馨旅館，提供寧靜的環境和戶外活動。享受遠足小徑、溫暖壁爐和鄉村魅力。適合自然愛好者。"
            },
            new Hotel
            {
                HotelId = 4,
                HotelName = "市中心酒店",
                Description = "位於繁華的市中心，這家酒店提供現代化客房並可輕鬆前往熱門景點。享受購物、餐飲和門口的娛樂活動。適合城市探險者。"
            },
            new Hotel
            {
                HotelId = 5,
                HotelName = "海洋微風酒店",
                Description = "這家海濱酒店擁有寬敞的客房和多樣的水上運動。放鬆在泳池旁，享用海景餐飲，感受海洋微風。適合熱帶度假。"
            },
            new Hotel
            {
                HotelId = 6,
                HotelName = "皇家花園",
                Description = "一座擁有典雅裝飾和茂盛花園的宏偉酒店，為賓客提供皇室般的體驗。享受下午茶、豪華套房和無可挑剔的服務。適合特殊場合。"
            },
            new Hotel
            {
                HotelId = 7,
                HotelName = "湖畔休憩處",
                Description = "一座位於湖邊的迷人休憩處，提供寧靜的環境和舒適的住宿。賓客可享受釣魚、划船和湖邊野餐。適合週末度假。"
            },
            new Hotel
            {
                HotelId = 8,
                HotelName = "歷史市中心旅館",
                Description = "位於一座歷史建築內，這家旅館將舊世界魅力與現代舒適相結合。探索附近的博物館，享用復古餐廳美食，欣賞獨特建築。適合歷史愛好者。"
            },
            new Hotel
            {
                HotelId = 9,
                HotelName = "天際套房",
                Description = "每間套房均享有城市全景，這家酒店適合喜愛城市景觀的人士。享受豪華設施、屋頂酒吧和靠近景點的便利。豪華且現代。"
            },
            new Hotel
            {
                HotelId = 10,
                HotelName = "綠谷小屋",
                Description = "坐落於風景如畫的山谷中，這家小屋提供寧靜放鬆的環境。賓客可探索遠足小徑，享受溫暖壁爐，放鬆在大自然中。適合充滿自然風情的假期。"
            },
            new Hotel
            {
                HotelId = 11,
                HotelName = "日出酒店",
                Description = "從這家酒店的陽台房間體驗令人驚嘆的日出。賓客可享受現代化設施、健身中心和免費早餐。提供現代便利和優質服務。"
            },
            new Hotel
            {
                HotelId = 12,
                HotelName = "河畔酒店",
                Description = "位於河邊，這家酒店提供美麗的景色和寧靜的氛圍。賓客可享受河邊餐飲、遊船和風景優美的散步。適合放鬆住宿。"
            },
            new Hotel
            {
                HotelId = 13,
                HotelName = "海濱度假村",
                Description = "一家位於海邊的豪華度假村，提供多種娛樂活動和高級餐飲。享受海灘通道、水上運動和美食。適合家庭和情侶。"
            },
            new Hotel
            {
                HotelId = 14,
                HotelName = "頂峰酒店",
                Description = "位於城市最高點，這家酒店提供壯麗景色和一流設施。賓客可享受屋頂泳池、高級餐飲和豪華套房。適合豪華住宿。"
            },
            new Hotel
            {
                HotelId = 15,
                HotelName = "城市綠洲酒店",
                Description = "位於城市中的一片綠洲，提供放鬆環境和現代設施。賓客可在spa放鬆，享受屋頂花園，時尚用餐。適合商務和休閒。"
            },
            new Hotel
            {
                HotelId = 16,
                HotelName = "復古旅館",
                Description = "一家擁有復古裝飾和溫馨氛圍的迷人旅館。享受古董傢俱、溫暖壁爐和個性化服務。適合欣賞經典風格和舒適的人士。"
            },
            new Hotel
            {
                HotelId = 17,
                HotelName = "海濱天堂",
                Description = "一家擁有壯麗海景和豪華設施的海濱酒店。賓客可在私人海灘放鬆，享受水上運動，並觀景用餐。適合熱帶度假。"
            },
            new Hotel
            {
                HotelId = 18,
                HotelName = "宏偉酒店",
                Description = "體驗這家宏偉酒店的奢華，提供壯麗建築和世界級服務。享受典雅套房、豪華spa和高級餐飲。適合奢華住宿。"
            },
            new Hotel
            {
                HotelId = 19,
                HotelName = "森林避風港",
                Description = "位於茂密森林中的寧靜休憩處，提供和平與安寧。賓客可探索自然小徑，壁爐旁放鬆，享受鄉村魅力。適合自然愛好者。"
            },
            new Hotel
            {
                HotelId = 20,
                HotelName = "沙漠幻景酒店",
                Description = "位於沙漠中的一家獨特酒店，提供壯麗景色和豪華住宿。享受沙漠之旅、清涼泳池和高級餐飲。適合異國情調的逃逸。"
            },
            new Hotel
            {
                HotelId = 21,
                HotelName = "港口酒店",
                Description = "位於港口旁，這家酒店提供美麗的水邊景色和現代設施。賓客可享受遊船、海鮮餐飲和風景散步。適合放鬆住宿。"
            },
            new Hotel
            {
                HotelId = 22,
                HotelName = "山間小屋",
                Description = "一座位於山中的鄉村小屋，提供舒適住宿和戶外冒險。享受遠足、釣魚和溫暖壁爐。適合自然逃逸。"
            },
            new Hotel
            {
                HotelId = 23,
                HotelName = "城市燈光酒店",
                Description = "位於市中心，這家酒店提供現代化客房並可輕鬆前往夜生活場所。享受時尚酒吧、美食餐廳和充滿活力的娛樂。適合城市探險者。"
            },
            new Hotel
            {
                HotelId = 24,
                HotelName = "花園旅館",
                Description = "一家擁有美麗花園和寧靜氛圍的迷人旅館。賓客可在花園放鬆，享受下午茶，舒適房間內休息。適合放鬆度假。"
            },
            new Hotel
            {
                HotelId = 25,
                HotelName = "海景度假村",
                Description = "一家擁有壯麗海景和一流設施的豪華度假村。賓客可在海灘放鬆，享受水上運動，觀景用餐。適合海灘假期。"
            },
            new Hotel
            {
                HotelId = 26,
                HotelName = "皇家套房",
                Description = "體驗這家酒店的奢華，提供寬敞套房和世界級服務。享受典雅裝飾、豪華spa和高級餐飲。適合奢華住宿。"
            },
            new Hotel
            {
                HotelId = 27,
                HotelName = "湖畔酒店",
                Description = "位於湖邊，這家酒店提供寧靜環境和現代舒適。賓客可享受釣魚、划船和湖邊野餐。適合寧靜休憩。"
            },
            new Hotel
            {
                HotelId = 28,
                HotelName = "城市酒店",
                Description = "一家位於市中心的現代酒店，提供時尚客房和優質服務。享受現代設施、健身中心和靠近景點的便利。適合商務和休閒。"
            },
            new Hotel
            {
                HotelId = 29,
                HotelName = "日落旅館",
                Description = "從這家溫馨旅館欣賞美麗的日落，提供舒適住宿和放鬆氛圍。賓客可在泳池旁放鬆，露台上用餐，享受風景。適合寧靜度假。"
            },
            new Hotel
            {
                HotelId = 30,
                HotelName = "大度假村",
                Description = "一家提供豪華設施和壯麗景色的宏偉度假村。享受寬敞客房、spa和高級餐飲。適合難忘的假期。"
            }
        };

        foreach (var hotel in hotels)
        {
            try
            {
                // Generate and set the DescriptionEmbedding for the current hotel
                hotel.DescriptionEmbedding = await GenerateEmbeddingAsync(hotel.Description);
                // Upsert the hotel to the collection
                await _hotelCollection.UpsertAsync(hotel);
                _logger.LogInformation(
                    $"Hotel ID: {hotel.HotelId}, Hotel Name: {hotel.HotelName} - Successfully uploaded");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing hotel {hotel.HotelId} ({hotel.HotelName}): {ex.Message}");
            }
        }
    }

    public async Task<List<HotelSearchResult>> GenerateEmbeddingsAndVectorSearchAsync(string text, int top = 10)
    {
        List<HotelSearchResult> hotelSearchResults = new List<HotelSearchResult>();

        // Generate the embedding.
        ReadOnlyMemory<float> searchEmbedding =
            await GenerateEmbeddingAsync(text);

        var options = new VectorSearchOptions<Hotel>
        {
            Top = top,
        };

        // Search using the already generated embedding.
        VectorSearchResults<Hotel> searchResult =
            await _hotelCollection.VectorizedSearchAsync(searchEmbedding, options);

        await foreach (var result in searchResult.Results)
        {
            _logger.LogInformation(
                $"Score: {result.Score}, Hotel ID: {result.Record.HotelId}, Hotel Name: {result.Record.HotelName}, Hotel Description: {result.Record.Description}");
            
            hotelSearchResults.Add(new HotelSearchResult
            {
                Score = result.Score ?? 0,
                HotelId = result.Record.HotelId,
                HotelName = result.Record.HotelName,
                Description = result.Record.Description
            });
        }

        return hotelSearchResults;
    }

    public async Task<List<HotelSearchResult>> GenerateEmbeddingsAndHybridSearchAsync(string text, string[] keywords)
    {
        List<HotelSearchResult> hotelSearchResults = new List<HotelSearchResult>();

        // Generate the embedding.
        ReadOnlyMemory<float> searchEmbedding =
            await GenerateEmbeddingAsync(text);

        var options = new HybridSearchOptions<Hotel>
        {
            Top = 10,
            AdditionalProperty = h => h.Description
        };

        
        // Search using the already generated embedding.
        VectorSearchResults<Hotel> searchResult =
            await _hotelKeywordHybridSearch.HybridSearchAsync(searchEmbedding, keywords, options);

        await foreach (var result in searchResult.Results)
        {
            _logger.LogInformation(
                $"Score: {result.Score}, Hotel ID: {result.Record.HotelId}, Hotel Name: {result.Record.HotelName}, Hotel Description: {result.Record.Description}");
            
            hotelSearchResults.Add(new HotelSearchResult
            {
                Score = result.Score ?? 0,
                HotelId = result.Record.HotelId,
                HotelName = result.Record.HotelName,
                Description = result.Record.Description
            });
        }

        return hotelSearchResults;
    }
}