using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public interface IGenreRepository
    {
        List<GenreVM> GetAllAsync();
        Task<Result<Genre>> InsertAsync(string name);
        Task<Result<Genre>> RemoveAsync(int id);
        Task<Result<Genre>> UpdateAsync(GenreDTO model);
    }
}