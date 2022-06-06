using Birlik.Data.Models;

namespace Birlik.Core.Interfaces
{
    public interface ICityRepository : IAsyncRepository<City>
    {
        public Task<City> GetAsync(string cityName);
    }
}