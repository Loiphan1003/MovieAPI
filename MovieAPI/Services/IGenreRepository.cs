using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IGenreRepository
    {
        List<GenreDTO> GetAll();
        List<GenreDTO> GetAllByIdMovie(Guid movieId);

        Genre Add(GenreVM genreVM);

        GenreDTO RemoveById(Guid id);
    }
}
