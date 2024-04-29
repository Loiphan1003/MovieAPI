using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenresController : ControllerBase
    {
        // private readonly IMovieGenreRepository _movieGenreRepository;

        // public MovieGenresController(IMovieGenreRepository movieGenreRepository)
        // {
        //     _movieGenreRepository = movieGenreRepository;
        // }

        // [Authorize]
        // [HttpPost("add-one")]
        // public IActionResult AddOne(MovieGenreVM movieGenre)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }

        //     var res = _movieGenreRepository.AddOne(movieGenre);

        //     if(res == false)
        //     {
        //         return BadRequest();
        //     }

        //     return Ok();
        // }
    }
}
