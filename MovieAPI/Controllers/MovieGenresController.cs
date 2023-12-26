using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenresController : ControllerBase
    {
        private readonly IMovieGenreRepository _movieGenreRepository;

        public MovieGenresController(IMovieGenreRepository movieGenreRepository)
        {
            _movieGenreRepository = movieGenreRepository;
        }

        [HttpPost]
        public IActionResult AddMany(string nameMovie, List<GenreVM> genres)
        {
            var res = _movieGenreRepository.AddManyGenres(nameMovie, genres);
            return Ok();
        }

        [HttpPost("add-one")]
        public IActionResult AddOne(MovieGenreVM movieGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = _movieGenreRepository.AddOne(movieGenre);

            if(res == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
