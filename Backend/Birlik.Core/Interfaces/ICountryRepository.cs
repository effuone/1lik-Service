using Birlik.Data.Models;

namespace Birlik.Core.Interfaces
{
    public interface ICountryRepository : IAsyncRepository<Country>
    {
        Task<Country> GetAsync(string countryName);
    }
}