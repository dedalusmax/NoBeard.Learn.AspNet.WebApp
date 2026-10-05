using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public class MovieRepository(AppDbContext dbContext) : IMovieRepository
{
    public async Task<List<Movie>> GetAllAsync()
    {
        return await dbContext.Movies.ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await dbContext.Movies.FindAsync(id);
    }  
    
    public async Task<int> CreateAsync(Movie movie)
    {
        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync();
        return movie.Id;
    }

    public async Task UpdateAsync(Movie movie)
    {
        var entity = await GetByIdAsync(movie.Id);

        if (entity is null)
        {
            throw new InvalidOperationException($"Movie with ID {movie.Id} not found.");
        }

        entity.Name = movie.Name;
        entity.Genre = movie.Genre;
        entity.ReleaseYear = movie.ReleaseYear;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await GetByIdAsync(id);

        if (movie is null)
        {
            throw new InvalidOperationException($"Movie with ID {id} not found.");
        }

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync();
    }
}
