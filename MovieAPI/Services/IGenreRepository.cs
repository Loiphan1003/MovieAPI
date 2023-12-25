using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IGenreRepository
    {
        List<Genre> GetAll();

        Genre Add(GenreVM genreVM);

        void Remove(string id);
    }
}
