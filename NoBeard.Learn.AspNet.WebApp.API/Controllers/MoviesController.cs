using Microsoft.AspNetCore.Mvc;
using NoBeard.Learn.AspNet.WebApp.Data;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace NoBeard.Learn.AspNet.WebApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(AppDbContext dbContext) : ControllerBase
{
    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAsync()
    {
        var result = await dbContext.Movies.ToListAsync();

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
        var movie = dbContext.Movies.Find(id);

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
        dbContext.Movies.Add(movie);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // PUT api/movies/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Movie model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var movie = await dbContext.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        movie.Name = model.Name;
        movie.Genre = model.Genre;
        movie.ReleaseYear = model.ReleaseYear;

        // dbContext.Entry(model).State = EntityState.Modified;

        await dbContext.SaveChangesAsync();

        return NoContent(); // Ok()
    }

    // DELETE api/movies/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var movie = await dbContext.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        dbContext.Movies.Remove(movie);
        await dbContext.SaveChangesAsync();

        return NoContent(); // Ok()
    }
}
