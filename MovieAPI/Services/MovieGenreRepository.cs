using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Migrations;

namespace MovieAPI.Services
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly MovieContext _context;
        private readonly StoreProcedure _storeProcedure;

        public MovieGenreRepository(MovieContext context, StoreProcedure storeProcedure)
        {
            _context = context;
            _storeProcedure = storeProcedure;
        }

        public List<MovieGenre> AddManyGenres(string movieId, List<GenreVM> genres)
        {
            throw new NotImplementedException();
        }

        public bool AddOne(MovieGenreVM movieGenre)
        {
            try
            {
                var movie = _context.Movies.FirstOrDefault(m => m.Title.Equals(movieGenre.MovieName));
                var genre = _context.Genres.FirstOrDefault(g => g.Name.Equals(movieGenre.GenreName));

                if(movie == null || genre == null)
                {
                    return false;
                }

                MovieGenre addMovieGenre = new MovieGenre
                {
                    GenreId = genre.Id,
                    MovieId = movie.Id
                };
                _context.MovieGenres.Add(addMovieGenre);
                _context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
