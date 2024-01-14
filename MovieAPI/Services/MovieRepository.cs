using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public RepositoryResult Add(MovieVM movie)
        {
            try
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
                return new RepositoryResult(true,"200", "More successful movies");
            }
            catch
            {
                return new RepositoryResult(false, "500", "Add Erros");
            }
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

            var movies = query
                        .Include(m => m.Persons)
                        .Include(m => m.Genres)
                        .Select(m => _mapper.Map<MovieDTO>(m))
                        .ToList();

            return movies;
        }

        public RepositoryResult Update(MovieUpdate movie)
        {
            try
            {
                var res = _context.Movies.FirstOrDefault(m => m.Id.Equals(movie.Id));

                if (res == null)
                {
                    return new RepositoryResult(false,"404", "Movie updates fail, movies don't exist");
                }

                res.Title = movie.Title;
                res.Budget = movie.Budget;
                res.DateRelease = movie.DateRelease;
                res.IMDbRate = movie.IMDbRate;
                res.Runtime = movie.Runtime;

                _context.Movies.Update(res);
                _context.SaveChanges();
                return new RepositoryResult(true,"200", "Successful movie updates");
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false,"500", ex.ToString());
            }
        }

        public RepositoryResult Delete(Guid id)
        {
            var res = _context.Movies
                                .FirstOrDefault(m => m.Id.Equals(id));
            if (res == null)
            {
                return new RepositoryResult(false, "404" ,"Movie deletion failed, movie Id does not exist");
            }
            _context.Movies.Remove(res);
            _context.SaveChanges();
            return new RepositoryResult(true,"200", "Successfully delete movies");
        }

    }
}
