using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAll(QueryObject query);
        Movie Add(MovieVM movie);
        MovieUpdate Update(MovieUpdate movie);
        Movie Delete(Guid id);
    }
}
