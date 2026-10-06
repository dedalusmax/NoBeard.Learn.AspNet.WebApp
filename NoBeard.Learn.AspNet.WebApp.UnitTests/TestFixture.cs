using NoBeard.Learn.AspNet.WebApp.API.Controllers;
using NoBeard.Learn.AspNet.WebApp.Data;
using NoBeard.Learn.AspNet.WebApp.Data.Repositories;

namespace NoBeard.Learn.AspNet.WebApp.UnitTests;

public class TestFixture : IDisposable
{
    internal MoviesController Controller { get; init; }

    public TestFixture()
    {
        var dbContext = new AppDbContext();
        var repository = new MovieRepository(dbContext);
        Controller = new MoviesController(repository);
    }

    public void Dispose()
    {
        // Cleanup code here, if needed
    }
}
