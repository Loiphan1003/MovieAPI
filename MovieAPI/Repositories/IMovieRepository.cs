using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<List<MovieDTO>> GetAllAsync();
        Task<Result<Movie>> InsertAsync(MovieVM model);
        Task<List<CastVM>> GetCastAsync(string name);
        Task<Result<MovieGenres>> InsertGenreAsync(MovieGenreVM model);
        Task<Result<Cast>> InsertCastAsync(CastAdd model);
        Task<Result<CastAdd>> AddCastAsync(CastAdd model);

    }
}