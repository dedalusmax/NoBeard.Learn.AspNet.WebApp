using NoBeard.Learn.AspNet.WebApp.Data.Entities;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllAsync();
    
    Task<Movie?> GetByIdAsync(int id);

    Task<int> CreateAsync(Movie movie);

    Task UpdateAsync(Movie movie);

    Task DeleteAsync(int id);
}
