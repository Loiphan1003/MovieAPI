using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] QueryObject queryObject)
        {
            try
            {
                var res = _personRepository.GetAll(queryObject);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex}");
            }
        }

        [HttpGet("name={name}")]
        public IActionResult GetByName(string name)
        {
            var res = _personRepository.GetByName(name);
            if (res.Count == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPerson(PersonVM person)
        {
            var res = _personRepository.Add(person);
            return Ok(res);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(PersonUpdate person)
        {
            var res = _personRepository.Update(person);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _personRepository.Delete(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
