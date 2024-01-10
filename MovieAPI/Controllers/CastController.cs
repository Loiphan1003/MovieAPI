using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private ICastRepository _castRepository;

        public CastController(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_castRepository.GetAll());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOne(CastVM cast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = _castRepository.AddOne(cast);
            if(res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
