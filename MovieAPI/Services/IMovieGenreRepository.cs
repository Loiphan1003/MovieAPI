using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IMovieGenreRepository
    {
        List<MovieGenre> AddManyGenres(string movieId, List<GenreVM> genres);

        bool AddOne(MovieGenreVM movieGenre);
    }
}
