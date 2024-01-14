using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IMapper _mapper;
        private readonly MovieContext _context;
        private readonly StoreProcedure _storeProcedure;

        public GenreRepository(MovieContext context, StoreProcedure storeProcedure, IMapper mapper)
        {
            _context = context;
            _storeProcedure = storeProcedure;
            _mapper = mapper;
        }

        public RepositoryResult Add(GenreVM genreVM)
        {
            try
            {
                var existGenre = _context.Genres.FirstOrDefault(g => g.Name.Equals(genreVM.Name));

                if (existGenre != null)
                {
                    return new RepositoryResult(false,"404", "The film genre already exists");
                }

                Genre genre = new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = genreVM.Name
                };

                _context.Genres.Add(genre);
                _context.SaveChanges();

                //_context.Database.ExecuteSqlRaw("execute InstertNewGenre @Id, @Name",
                //    new SqlParameter("@Id", genre.Id),
                //    new SqlParameter("@Name",genre.Name)
                //    );

                return new RepositoryResult(true,"200", "More successful movie genres");
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false,"500", ex.ToString());
            }
        }

        public List<GenreDTO> GetAll(QueryObject q)
        {
            var query = _context.Genres.AsQueryable();

            #region Filter Name
            if (!string.IsNullOrEmpty(q.Search))
            {
                query = query.Where(g => g.Name.Contains(q.Search));
            }
            #endregion

            #region Sort
            if (!string.IsNullOrEmpty(q.SortBy))
            {
                switch (q.SortBy)
                {
                    case "name":
                        query = query.OrderBy(g => g.Name);
                        break;
                }
            }
            #endregion

            #region Paging
            if (q.PageSize > 0 && q.Page > 0)
            {
                query = query.Skip((q.Page - 1) * q.PageSize).Take(q.PageSize);
            }
            #endregion

            var genres = query.Select(g => _mapper.Map<GenreDTO>(g)).ToList();

            return genres;
        }

        public List<GenreDTO> GetAllByIdMovie(Guid movieId)
        {
            var query = _context.Genres
                  .FromSql(_storeProcedure.FindGenreByMovieId(movieId))
                  .ToList();

            var res = query.Select(g => new GenreDTO { Name = g.Name, Id = g.Id });


            return res.ToList();
        }

        public RepositoryResult RemoveById(Guid id)
        {
            var res = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (res == null)
            {
                return new RepositoryResult(false,"404", "Film genres that do not exist");
            }
            _context.Genres.Remove(res);
            _context.SaveChanges();

            return new RepositoryResult(true,"200", "Successfully remove movie genres");
        }

        public RepositoryResult Update(GenreDTO genre)
        {
            var res = _context.Genres.FirstOrDefault(g => g.Id.Equals(genre.Id));
            if (res == null)
            {
                return new RepositoryResult(false,"404", "Film genres that do not exist");
            }

            res.Name = genre.Name;
            _context.Genres.Update(res);
            _context.SaveChanges();

            return new RepositoryResult(true,"200", "Successful movie genre update");
        }
    }
}
