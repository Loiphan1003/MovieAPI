using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IGenreRepository
    {
        List<GenreDTO> GetAll(QueryObject query);
        List<GenreDTO> GetAllByIdMovie(Guid movieId);

        RepositoryResult Add(GenreVM genreVM);
        RepositoryResult Update(GenreDTO genre);
        RepositoryResult RemoveById(Guid id);
    }
}
