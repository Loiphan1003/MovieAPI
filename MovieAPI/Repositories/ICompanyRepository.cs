using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public interface ICompanyRepository
    {
        List<Company> GetAllAsync();
        Task<Result<Company>> InsertAsync(string name);
        Task<Result<Company>> RemoveAsync(int id);
        Task<Result<Company>> UpdateAsync(CompanyDTO model);
    }
}