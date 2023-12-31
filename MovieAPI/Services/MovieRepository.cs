using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly StoreProcedure _storeProcedure;
        private readonly IGenreRepository _genreRepository;
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;

        public MovieRepository(MovieContext context, StoreProcedure storeProcedure, IGenreRepository genreRepository, IMapper mapper, ICastRepository castRepository)
        {
            _context = context;
            _storeProcedure = storeProcedure;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _castRepository = castRepository;
        }

        public Movie Add(MovieVM movie)
        {
            Movie newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = movie.Title,
                Budget = movie.Budget,
                DateRelease = movie.DateRelease,
                IMDbRate = movie.IMDbRate,
                Runtime = movie.Runtime
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return newMovie;
        }

        public List<MovieDTO> GetAll(QueryObject queryObject)
        {
            var query = _context.Movies.AsQueryable();

            #region Filter Name Movie
            if (!string.IsNullOrEmpty(queryObject.Search))
            {
                query = query.Where(m => m.Title.Contains(queryObject.Search));
            }
            #endregion

            #region Sort By
            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "imdb_rate_desc":
                        query = query.OrderByDescending(m => m.IMDbRate);
                        break;
                    case "imdb_rate_asc":
                        query = query.OrderBy(m => m.IMDbRate);
                        break;
                    case "budget_desc":
                        query = query.OrderByDescending(m => m.Budget);
                        break;
                    case "budget_asc":
                        query = query.OrderBy(m => m.Budget);
                        break;
                }
            }
            #endregion

            #region Paging
            if (queryObject.Page > 0 && queryObject.PageSize > 0)
            {
                query = query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
            #endregion

            var movies = query.Select(m => _mapper.Map<MovieDTO>(m)).ToList();

            if (movies == null)
            {
                return [];
            }

            foreach (var item in movies)
            {
                item.Genres = _genreRepository.GetAllByIdMovie(item.Id);
                item.Casts = _castRepository.GetAllCastByMovieId(item.Id);
            }

            return movies;
        }

        public MovieUpdate Update(MovieUpdate movie)
        {
            try
            {
                var res = _context.Movies.FirstOrDefault(m => m.Id.Equals(movie.Id));

                if (res == null)
                {
                    return null;
                }

                res.Title = movie.Title;
                res.Budget = movie.Budget;
                res.DateRelease = movie.DateRelease;
                res.IMDbRate = movie.IMDbRate;
                res.Runtime = movie.Runtime;

                _context.Movies.Update(res);
                _context.SaveChanges();
                return movie;
            }
            catch
            {
                return null;
            }
        }

        public Movie Delete(Guid id)
        {
            var res = _context.Movies
                .FirstOrDefault(m => m.Id.Equals(id));
            if (res == null)
            {
                return null;
            }
            _context.Movies.Remove(res);
            _context.SaveChanges();
            return res;
        }

    }
}
