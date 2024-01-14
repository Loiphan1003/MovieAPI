using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Migrations;

namespace MovieAPI.Services
{
    public class CastRepository : ICastRepository
    {
        private readonly IMapper _mapper;
        private readonly MovieContext _context;
        private readonly StoreProcedure _storeProcedure;

        public CastRepository(MovieContext context, StoreProcedure storeProcedure, IMapper mapper)
        {
            _context = context;
            _storeProcedure = storeProcedure;
            _mapper = mapper;
        }

        public Cast AddOne(CastVM cast)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title.Equals(cast.MovieName));
            var person = _context.Persons.FirstOrDefault(p => p.Name.Equals(cast.PersonName));

            if (movie == null || person == null) {
                return null;
            }

            Cast newCast = new Cast
            {
                MovieId = movie.Id,
                PersonId = person.Id,
                Character = cast.Character
            };
            _context.Casts.Add(newCast);
            _context.SaveChanges();
            return newCast;
        }

        public List<Cast> GetAll()
        {
            return _context.Casts.ToList();
        }

        //public List<CastDTO> GetAllCastByMovieId(Guid movieId)
        //{
        //    var res = _context.Casts
        //        .Where(c => c.MovieId.Equals(movieId))
        //        .Select(c => new CastDTO
        //        {
        //            CharacterName = c.CharacterName,
        //            Id = c.PersonId,
        //        })
        //        .ToList();

        //    foreach (var item in res)
        //    {
        //        var person = _context.Persons.FirstOrDefault(p => p.Id.Equals(item.Id));
        //        item.Name = person.Name;
        //    }

        //    return res;
        //}
    }
}
