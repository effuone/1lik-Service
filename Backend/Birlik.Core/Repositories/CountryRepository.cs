using Birlik.Core.DataAccess;
using Birlik.Core.Interfaces;
using Birlik.Data.Context;
using Birlik.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Birlik.Core.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly BirlikAppContext _context;
        private readonly DapperContext _dapper;

        public CountryRepository(BirlikAppContext context, DapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<int> CreateAsync(Country model)
        {
            await _context.Countries.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CountryId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> GetAsync(string countryName)
        {
            return await _context.Countries.Where(x=>x.CountryName == countryName).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, Country model)
        {
            var entity = await _context.Countries.FindAsync(id);
            entity.CountryName = model.CountryName;
            await _context.SaveChangesAsync();
        }
    }
}