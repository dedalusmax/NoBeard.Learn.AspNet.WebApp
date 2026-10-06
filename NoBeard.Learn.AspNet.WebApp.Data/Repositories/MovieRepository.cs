using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebApp.Data.Models;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public sealed class MovieRepository(AppDbContext dbContext) : BaseRepository<Movie>(dbContext), IMovieRepository
{
    public async Task ActivateAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            throw new InvalidOperationException($"Movie with Id {id} not found.");
        }

        entity.IsActive = true;

        dbContext.Movies.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetFilteredAsync(string name, bool ascending, int page, int pageSize)
    {
        var query = dbContext.Movies.AsQueryable(); // as IQueryable<Movie>;

        // filtering
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(m => m.Name.Contains(name));
        }

        // sorting
        query = ascending ? query.OrderBy(m => m.Name) : query.OrderByDescending(m => m.Name);

        // paging
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

    protected override IQueryable<Movie> ApplyFilters(IQueryable<Movie> query, QueryParameters parameters)
    {
        var filters = parameters as MovieQueryParameters;

        if (filters is not null)
        {
            if (!string.IsNullOrEmpty(filters.Title))
            {
                query = query.Where(m => m.Name.Contains(filters.Title));
            }
            if (filters.ReleaseYear.HasValue)
            {
                query = query.Where(m => m.ReleaseYear == filters.ReleaseYear.Value);
            }
        }

        return query;
    }
}
