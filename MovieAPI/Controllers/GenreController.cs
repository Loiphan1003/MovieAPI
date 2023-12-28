using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _genreRepository.GetAll();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AddOne(GenreVM genreVM)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = _genreRepository.Add(genreVM);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _genreRepository.RemoveById(id);
            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut]
        public IActionResult Update(GenreDTO genre)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = _genreRepository.Update(genre);
            if(res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
