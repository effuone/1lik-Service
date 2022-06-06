using Birlik.Core.DataAccess;
using Birlik.Core.Interfaces;
using Birlik.Data.Context;
using Birlik.Data.Models;
using Microsoft.EntityFrameworkCore;

public class LocationRepository : ILocationRepository
{
    private readonly DapperContext _dapper;
    private readonly BirlikAppContext _context;

    public LocationRepository(DapperContext dapper, BirlikAppContext context)
    {
        _dapper = dapper;
        _context = context;
    }

    public async Task<int> CreateAsync(Location model)
    {
        await _context.Locations.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.LocationId;
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _context.Locations.FindAsync(id);
        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location> GetAsync(int id)
    {
        return await _context.Locations.FindAsync(id);
    }

    public async Task UpdateAsync(int id, Location model)
    {
        var existingModel = await _context.Locations.FindAsync(id);
        existingModel = model;
        await _context.SaveChangesAsync();
    }
}