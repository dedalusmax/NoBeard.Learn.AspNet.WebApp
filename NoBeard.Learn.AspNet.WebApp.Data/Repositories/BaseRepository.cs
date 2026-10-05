using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using NoBeard.Learn.AspNet.WebApp.Data.Models;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext dbContext;

    protected BaseRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<int> CreateAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(TEntity model)
    {
        var entity = await GetByIdAsync(model.Id);

        if (entity is null)
        {
            throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with Id {model.Id} not found.");
        }

        // dbContext.Entry(model).State = EntityState.Modified;
        dbContext.Entry(entity).CurrentValues.SetValues(model);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with Id {id} not found.");
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<QueryResult<TEntity>> GetAsync(QueryParameters parameters)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();

        // Apply filtering
        if (!string.IsNullOrEmpty(parameters.Search))
        {
            query = query.Where(e => EF.Functions.Like(e.ToString(), $"%{parameters.Search}%"));
        }
        // Apply sorting
        if (!string.IsNullOrEmpty(parameters.SortBy))
        {
            query = parameters.Descending
                ? query.OrderByDescending(e => EF.Property<object>(e, parameters.SortBy))
                : query.OrderBy(e => EF.Property<object>(e, parameters.SortBy));
        }

        var totalCount = await query.CountAsync();

        // Apply pagination
        var items = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new QueryResult<TEntity>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }
}
