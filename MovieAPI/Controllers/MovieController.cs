﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetAll([FromQuery] QueryObject query)
        {
            try
            {
                var res = _movieRepository.GetAll(query);
                return Ok(res);
            }
            catch(Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(MovieVM movieVM)
        {
            var res = _movieRepository.Add(movieVM);
            return Ok(res);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateOne(MovieUpdate movie)
        {
            try
            {
                var res = _movieRepository.Update(movie);
                if (res.Success == false)
                {
                    return Ok(res);
                }

                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var res = _movieRepository.Delete(id);
            if (res.Success == false)
            {
                return Ok(res);
            }
            return Ok(res);
        }
    }
}
