using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MovieContext _context;

        public CompanyRepository(MovieContext context)
        {
            _context = context;
        }

        public List<Company> GetAllAsync()
        {
            var companies = _context.Companies.ToList();
            return companies;
        }

        public async Task<Result<Company>> InsertAsync(string name)
        {
            var companyExist = await _context.Companies.FirstOrDefaultAsync(c => c.Name.Equals(name));

            if (companyExist != null)
            {
                return Result<Company>.Failure(Errors.AlreadyCreate($"{name} has been created"));
            }

            var company = new Company { Name = name };

            _context.Companies.Add(company);
            _context.SaveChanges();

            return Result<Company>.Success("Insert successful", default!);
        }

        public async Task<Result<Company>> RemoveAsync(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return Result<Company>.Failure(Errors.NotFound("Id not found"));
            }

            _context.Companies.Remove(company);
            _context.SaveChanges();

            return Result<Company>.Success("Remove successfull", default!);
        }

        public async Task<Result<Company>> UpdateAsync(CompanyDTO model)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name.Equals(model.Name));

            if (company == null)
            {
                return Result<Company>.Failure(Errors.NotFound($"{model.Name} not found"));
            }

            company.Name = model.Name;
            _context.Companies.Update(company);
            _context.SaveChanges();

            return Result<Company>.Success("Update successful", default!);
        }
    }
}