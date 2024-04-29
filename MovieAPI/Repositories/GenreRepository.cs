using Mapster;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieContext _context;

        public GenreRepository(MovieContext context)
        {
            _context = context;
        }

        public List<GenreVM> GetAllAsync()
        {
            var genres = _context.Genres.ToList();

            return genres.Adapt<List<GenreVM>>();
        }


        // <sumary>
        // Add a new genre asynchronous
        // </sumary>
        // <param name="name"> The name of genre to be added </param>
        public async Task<Result<Genre>> InsertAsync(string name)
        {
            // Ensure the name is not null or empty
            if (string.IsNullOrEmpty(name))
            {
                return Result<Genre>.Failure(Errors.MissingData("Name is null"));
            }

            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (genre != null)
                {
                    return Result<Genre>.Failure(Errors.AlreadyCreate($"Genre {name} has created"));
                }

                genre = new Genre { Name = name };

                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();

                return Result<Genre>.Success($"Insert genre success", genre);
            }
            catch (Exception ex)
            {
                return Result<Genre>.Failure(Errors.InternalError(ex.ToString()));
            }
        }

        public async Task<Result<Genre>> RemoveAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return Result<Genre>.Failure(Errors.NotFound("Not found genre"));
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Result<Genre>.Success($"Remove genre {genre.Name} successful", default!);
        }

        public async Task<Result<Genre>> UpdateAsync(GenreDTO model)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == model.Id);

            if (genre == null)
            {
                return Result<Genre>.Failure(Errors.NotFound("Not found genre"));
            }

            genre.Name = model.Name;

            _context.Genres.Update(genre);
            _context.SaveChanges();

            return Result<Genre>.Success("Update successful", genre);
        }
    }
}