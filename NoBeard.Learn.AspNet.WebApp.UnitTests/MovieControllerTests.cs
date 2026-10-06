using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;

namespace NoBeard.Learn.AspNet.WebApp.UnitTests;

public class MovieControllerTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public async Task TestMovieController_GetMovies_Ok()
    {
        // Arrange

        // Act
        var result = await fixture.Controller.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ActionResult<IEnumerable<Movie>>>(result);
        Assert.IsType<ActionResult<IEnumerable<Movie>>>(result, exactMatch: true);
        Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        var actionResult = result.Result as OkObjectResult;
        Assert.NotNull(actionResult.Value);
        Assert.IsAssignableFrom<IEnumerable<Movie>>(actionResult.Value);
        var movies = actionResult.Value as IEnumerable<Movie>;
        Assert.NotEmpty(movies);
        Assert.Equal(8, movies.Count());
    }

    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(7)]
    public void TestMovieController_GetMovie_Ok(int movieId)
    {
        // Arrange

        // Act
        var result = fixture.Controller.GetMovie(movieId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ActionResult<Movie>>(result);
        Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        var actionResult = result.Result as OkObjectResult;
        Assert.NotNull(actionResult.Value);
        Assert.IsAssignableFrom<Movie>(actionResult.Value);
        var movie = actionResult.Value as Movie;
        Assert.Equal(movieId, movie.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(200)]
    public void TestMovieController_GetMovie_NotFound(int movieId)
    {
        // Arrange

        // Act
        var result = fixture.Controller.GetMovie(movieId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ActionResult<Movie>>(result);
        Assert.IsAssignableFrom<NotFoundResult>(result.Result);
    }
}
