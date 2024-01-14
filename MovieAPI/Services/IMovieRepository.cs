using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAll(QueryObject query);
        RepositoryResult Add(MovieVM movie);
        RepositoryResult Update(MovieUpdate movie);
        RepositoryResult Delete(Guid id);
    }
}
