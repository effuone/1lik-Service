namespace Birlik.Core.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T> GetAsync(int id); 
        Task<int> CreateAsync(T model); 
        Task UpdateAsync(int id, T model);
        Task DeleteAsync(int id);
    }
}