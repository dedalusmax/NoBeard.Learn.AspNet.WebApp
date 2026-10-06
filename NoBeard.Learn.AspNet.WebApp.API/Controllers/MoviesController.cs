using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using NoBeard.Learn.AspNet.WebApp.Data.Models;
using NoBeard.Learn.AspNet.WebApp.Data.Repositories;

namespace NoBeard.Learn.AspNet.WebApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMovieRepository repository) : ControllerBase
{
    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAsync()
    {
        var result = await repository.GetAllAsync();

        if (result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    //public async Task<IEnumerable<Movie>> GetAsync()
    //{
    //    return await dbContext.Movies.ToListAsync();
    //}

    [HttpGet("{name}/{ascending}/{page}/{pageSize}")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesFiltered(string name, bool ascending, int page, int pageSize)
    {
        var result = await repository.GetFilteredAsync(name, ascending, page, pageSize);

        return result.Count == 0 ? NoContent() : Ok(result);
    }

    [HttpGet("query")]
    public async Task<ActionResult<QueryResult<Movie>>> GetMovies([FromQuery] MovieQueryParameters parameters)
    {
        var result = await repository.GetAsync(parameters);
        return result.Items.Count == 0 ? NoContent() : Ok(result);
    }

    // GET api/movies/5
    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        var movie = repository.GetByIdAsync(id).Result;

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    // POST api/movies
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Movie movie)
    {
        var id = await repository.CreateAsync(movie);

        return CreatedAtAction("GetMovie", new { id }, movie);
    }

    // PUT api/movies/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Movie model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        try
        {
            await repository.UpdateAsync(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent(); // Ok()
    }

    // PUT api/movies/5/active
    [HttpPut("{id}/active")]
    public async Task<ActionResult> Activate(int id)
    {
        try
        {
            await repository.ActivateAsync(id);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent(); // Ok()
    }

    // DELETE api/movies/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await repository.DeleteAsync(id);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent(); // Ok()
    }
}
