using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Repositories;
using MovieAPI.Result;

namespace MovieAPI.Controllers
{
    [Route("api/genre")]
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
            var res = _genreRepository.GetAllAsync();
            return Ok(res);
        }

        // //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            if (int.TryParse(name, out _))
            {
                return BadRequest("Name is not a string");
            }

            var response = await _genreRepository.InsertAsync(name);

            if (response.IsFailure)
            {
                if (response.Error.code.Equals(Errors.Code.AlreadyCreate.ToString()))
                {
                    return Conflict(response.Error);
                }

                return BadRequest(response.Error.description);
            }

            return Ok(response.Message);
        }

        // //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()) || id <= 0)
            {
                return BadRequest();
            }

            var res = await _genreRepository.RemoveAsync(id);
            if (res.IsFailure)
            {
                return NotFound();
            }
            return Ok(res.Message);
        }

        // //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] GenreDTO genre)
        {
            if (!ModelState.IsValid || genre.Id <= 0)
            {
                return BadRequest();
            }

            var res = await _genreRepository.UpdateAsync(genre);

            if (res.IsFailure)
            {
                return NotFound();
            }

            return Ok(res.Message);
        }
    }
}
