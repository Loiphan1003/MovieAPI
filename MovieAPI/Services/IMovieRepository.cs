using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAll();
        Movie Add(MovieVM movie);
        List<MovieDTO> GetMovieByName(string nameMovie);
        MovieUpdate Update(MovieUpdate movie);
        Movie Delete(Guid id);

    }
}
