using Dotnet8QdrantVectorSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8QdrantVectorSearch.Services.Product;

public class ProductServiceByEfCore
{
    private readonly AvidaDbContext _dbContext;

    public ProductServiceByEfCore(AvidaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Building> GetBuildingByIdAsync(int id)
    {
        return await _dbContext.Buildings.AsNoTracking()
            .FirstOrDefaultAsync(x => x.BuildingId == id);
    }
    
    public async Task<List<Room>> GetRoomsByBuildingIdAsync(int buildingId)
    {
        return await _dbContext.Rooms.AsNoTracking()
            .Where(x => x.BuildingId == buildingId)
            .ToListAsync();
    }
}