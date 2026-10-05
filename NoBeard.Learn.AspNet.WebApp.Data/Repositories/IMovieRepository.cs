using NoBeard.Learn.AspNet.WebApp.Data.Entities;

namespace NoBeard.Learn.AspNet.WebApp.Data.Repositories;

public interface IMovieRepository : IBaseRepository<Movie>
{
    Task ActivateAsync(int id);
}
