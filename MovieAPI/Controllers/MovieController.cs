using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Repositories;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _movieRepository.GetAllAsync();
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route($"getcast")]
        public async Task<IActionResult> GetCast([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var res = await _movieRepository.GetCastAsync(name);

            return Ok(res);
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] MovieVM model)
        {
            var res = await _movieRepository.InsertAsync(model);
            return Ok(res);
        }

        [HttpPost]
        [Route("addcast")]
        public async Task<IActionResult> AddCast([FromForm] CastAdd model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _movieRepository.InsertCastAsync(model);

            if (res.IsFailure)
            {
                return Ok(res.Error);
            }

            return Ok(res.Message);
        }

        [HttpPost]
        [Route("addGenre")]
        public async Task<IActionResult> AddGenre([FromForm] MovieGenreVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _movieRepository.InsertGenreAsync(model);

            return Ok(response);
        }

        // [Authorize]
        // [HttpPut]
        // public IActionResult UpdateOne(MovieUpdate movie)
        // {
        //     try
        //     {
        //         var res = _movieRepository.Update(movie);
        //         if (res.Success == false)
        //         {
        //             return Ok(res);
        //         }

        //         return Ok(res);
        //     }
        //     catch
        //     {
        //         return BadRequest();
        //     }
        // }

        // [Authorize]
        // [HttpDelete]
        // public IActionResult Delete(Guid id)
        // {
        //     var res = _movieRepository.Delete(id);
        //     if (res.Success == false)
        //     {
        //         return Ok(res);
        //     }
        //     return Ok(res);
        // }
    }
}
