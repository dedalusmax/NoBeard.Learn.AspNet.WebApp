using NoBeard.Learn.AspNet.WebApp.Data.Entities;

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
}
