using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieContext _context;

        public GenreRepository(MovieContext context)
        {
            _context = context;
        }

        public Genre Add(GenreVM genreVM)
        {
            Genre genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = genreVM.Name
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            return genre;
        }

        public List<Genre> GetAll()
        {
            return _context.Genres.ToList();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
