using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Repositories;
using MovieAPI.Result;

namespace MovieAPI.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _personRepository.GetAllAsync();
            return Ok(res);
        }

        // [HttpGet("name={name}")]
        // public IActionResult GetByName(string name)
        // {
        //     var res = _personRepository.GetByName(name);
        //     if (res.Count == 0)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(res);
        // }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromForm] PersonAdd person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await _personRepository.AddAsync(person);
            if (res.Error.code.Equals(string.Empty))
            {
                string code = res.Error.code;

                if (code.Equals(Errors.Code.AlreadyCreate))
                {
                    return Conflict(res.Error.description);
                }
            }
            return Ok(res.Message);
        }

        // [Authorize]
        // [HttpPut]
        // public IActionResult Update(PersonUpdate person)
        // {
        //     var res = _personRepository.Update(person);
        //     if (res == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(res);
        // }

        // [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var res = await _personRepository.RemoveAsync(name);

            if (res.IsFailure)
            {
                return NotFound();
            }
            return Ok(res.Message);
        }
    }
}
