using Birlik.Core.DataAccess;
using Birlik.Core.Interfaces;
using Birlik.Data.Context;
using Birlik.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Birlik.Core.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly BirlikAppContext _context;
        private readonly DapperContext _dapper;

        public CityRepository(BirlikAppContext context, DapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<int> CreateAsync(City model)
        {
            await _context.Cities.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CityId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<City> GetAsync(string cityName)
        {
             return await _context.Cities.Where(x=>x.CityName == cityName).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, City model)
        {
           var entity = await _context.Cities.FindAsync(id);
            entity.CityName = model.CityName;
            await _context.SaveChangesAsync();
        }
    }
}