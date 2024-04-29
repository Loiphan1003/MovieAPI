using Microsoft.AspNetCore.Mvc;
using MovieAPI.Entities;
using MovieAPI.Repositories;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var companies = _companyRepository.GetAllAsync();

            return Ok(companies);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            if (int.TryParse(name, out _))
            {
                return BadRequest("Name is not string");
            }

            var result = await _companyRepository.InsertAsync(name);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CompanyDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _companyRepository.UpdateAsync(model);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            if (!int.TryParse(id.ToString(), out _) || id <= 0)
            {
                return BadRequest();
            }

            var result = await _companyRepository.RemoveAsync(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Message);
        }
    }
}