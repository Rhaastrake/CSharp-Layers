using Microsoft.AspNetCore.Mvc;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;

namespace MovieManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IGenericService<MovieModel> _movieService;

        public MoviesController(IGenericService<MovieModel> movieService)
        {
            _movieService = movieService;
        }

        // GET /api/movies
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<MovieModel>>> GetAll(CancellationToken cancellationToken)
        {
            var movies = await _movieService.GetAllAsync(cancellationToken);
            return Ok(movies);
        }

        // GET /api/movies/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieModel>> GetById(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieService.GetByIdAsync(id, cancellationToken);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST /api/movies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieModel>> Create([FromBody] MovieModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _movieService.CreateAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/movies/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] MovieModel model, CancellationToken cancellationToken)
        {
            if (id != model.Id)
            {
                return BadRequest("L'id della route non corrisponde all'id del body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _movieService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE /api/movies/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _movieService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}