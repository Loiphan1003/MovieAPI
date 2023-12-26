using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAll();
        Movie Add(MovieVM movie);

    }
}
