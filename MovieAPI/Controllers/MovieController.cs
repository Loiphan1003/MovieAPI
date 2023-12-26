using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Services;

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
        public IActionResult GetAll()
        {
            var res = _movieRepository.GetAll();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Add(MovieVM movieVM)
        {
            var res = _movieRepository.Add(movieVM);
            return Ok(res);
        }
    }
}
