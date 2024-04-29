using Mapster;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }
        public async Task<Result<Movie>> InsertAsync(MovieVM model)
        {
            var movieExit = await _context.Movies.FirstOrDefaultAsync(m => m.Title.Equals(model.Title));

            if (movieExit != null)
            {
                return Result<Movie>.Failure(Errors.AlreadyCreate($"{model.Title} has been created"));
            }

            Movie movie = model.Adapt<Movie>();

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Result<Movie>.Success("Insert successful", movie);
        }

        public async Task<List<MovieDTO>> GetAllAsync()
        {
            var movies = await _context.Movies
                        .Include(m => m.Genres)
                        .ToListAsync();

            return movies.Adapt<List<MovieDTO>>();
        }

        public async Task<Result<MovieGenres>> InsertGenreAsync(MovieGenreVM model)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Title.Equals(model.MovieName));

            if (movie == null)
            {
                return Result<MovieGenres>.Failure(Errors.NotFound("Movie not found"));
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(m => m.Name.Equals(model.GenreName));

            if (genre == null)
            {
                return Result<MovieGenres>.Failure(Errors.NotFound("Genre not found"));
            }

            var movieGenre = await _context.MovieGenres.FirstOrDefaultAsync(m => m.MovieId == movie.Id && m.GenreId == genre.Id);

            if (movieGenre != null)
            {
                return Result<MovieGenres>.Failure(Errors.AlreadyCreate($"{model.GenreName} has already add for {model.MovieName}"));
            }

            movieGenre = new MovieGenres
            {
                GenreId = genre.Id,
                MovieId = movie.Id
            };

            _context.MovieGenres.Add(movieGenre);
            _context.SaveChanges();

            return Result<MovieGenres>.Success("", default!);
        }

        public async Task<Result<Cast>> InsertCastAsync(CastAdd model)
        {
            if (model.Validate() == false)
            {
                return Result<Cast>.Failure(Errors.MissingData("Missing data"));
            }

            // Find movie
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Title.Equals(model.MovieName));
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Name.Equals(model.PersonName));

            if (movie == null)
            {
                return Result<Cast>.Failure(Errors.NotFound($"Movie {model.MovieName} is not found"));
            }
            if (person == null)
            {
                return Result<Cast>.Failure(Errors.NotFound($"Person {model.PersonName} is not found"));
            }

            var cast = new Cast
            {
                MovieId = movie.Id,
                PersonId = person.Id,
                Character = model.Character
            };

            _context.Casts.Add(cast);
            _context.SaveChanges();

            return Result<Cast>.Success("Insert cast succesful", default!);
        }

        public async Task<Result<CastAdd>> AddCastAsync(CastAdd model)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Title.Equals(model.MovieName));
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Name.Equals(model.PersonName));

            if (movie == null)
            {
                return Result<CastAdd>.Failure(Errors.NotFound($"Movie {model.MovieName} is not found"));
            }

            if (person == null)
            {
                return Result<CastAdd>.Failure(Errors.NotFound($"Person {model.PersonName} is not found"));
            }

            var cast = new Cast
            {
                Character = model.Character,
                MovieId = movie.Id,
                PersonId = person.Id
            };

            _context.Casts.Add(cast);
            await _context.SaveChangesAsync();

            return Result<CastAdd>.Success("Add cast successful", model);
        }

        public async Task<List<CastVM>> GetCastAsync(string name)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Title.Equals(name));

            if (movie == null)
            {
                return [];
            }

            List<CastVM> cast = await _context.Casts
                    .Include(c => c.Person)
                    .Where(c => c.MovieId == movie.Id)
                    .Select(c => new CastVM { Character = c.Character, Person = c.Person.Name })
                    .ToListAsync();

            return cast;
        }
    }
}