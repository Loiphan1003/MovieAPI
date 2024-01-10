using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetAll([FromQuery] QueryObject query)
        {
            var res = _genreRepository.GetAll(query);
            return Ok(res);
        }

        //[Authorize]
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

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var res = _genreRepository.RemoveById(id);
            if(res.Success == false)
            {
                return NotFound();
            }
            return Ok(res);
        }

        //[Authorize]
        [HttpPut]
        public IActionResult Update(GenreDTO genre)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = _genreRepository.Update(genre);
            if(res.Success == false)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
