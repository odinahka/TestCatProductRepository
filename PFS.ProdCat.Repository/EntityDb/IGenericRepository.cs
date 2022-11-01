namespace PFS.ProdCat.Repository.EntityDb
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> Exists<U>(U parameter);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync<U>(U parameter);
        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(List<T> entities);
    }
}