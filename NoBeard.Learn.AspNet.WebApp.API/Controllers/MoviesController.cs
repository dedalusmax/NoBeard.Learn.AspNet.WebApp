using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using NoBeard.Learn.AspNet.WebApp.Data.Repositories;

namespace NoBeard.Learn.AspNet.WebApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IBaseRepository<Movie> repository) : ControllerBase
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
