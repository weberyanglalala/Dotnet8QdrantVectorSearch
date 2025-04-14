using System.ComponentModel;
using Dotnet8QdrantVectorSearch.Models;
using Microsoft.SemanticKernel;

namespace Dotnet8QdrantVectorSearch.Services.Product;

public class ProductEFCorePlugin
{
    private readonly ProductServiceByEfCore _productService;

    public ProductEFCorePlugin(ProductServiceByEfCore productService)
    {
        _productService = productService;
    }

    [KernelFunction("GetBuildingById")]
    [Description("根據建築物ID獲取建築物單一詳細信息")]
    public async Task<Building> GetBuildingByIdAsync([Description("建築物 id")]int id)
    {
        return await _productService.GetBuildingByIdAsync(id);
    }

    [KernelFunction("GetRoomsByBuildingId")]
    [Description("根據建築物ID獲取建築物下所有房間")]
    public async Task<List<Room>> GetRoomsByBuildingIdAsync([Description("建築物 id")]int buildingId)
    {
        return await _productService.GetRoomsByBuildingIdAsync(buildingId);
    }
}