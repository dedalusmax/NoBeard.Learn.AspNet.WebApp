using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using NoBeard.Learn.AspNet.WebApp.Data.Models;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id);

    Task<int> CreateAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(int id);

    Task<QueryResult<TEntity>> GetAsync(QueryParameters parameters);
}
