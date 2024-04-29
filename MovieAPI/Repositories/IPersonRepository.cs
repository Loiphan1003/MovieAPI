using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Result<Person>> AddAsync(PersonAdd model);
        Task<Result<Person>> RemoveAsync(string name);
        Task<Result<Person>> UpdateAsync(PersonUpdate model);
    }
}