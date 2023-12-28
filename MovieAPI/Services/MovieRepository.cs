﻿using AutoMapper;
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

        public List<MovieDTO> GetMovieByName(string nameMovie)
        {
            var res = _context.Movies
                .Select(m => _mapper.Map<MovieDTO>(m))
                .AsEnumerable()
                .Where(m => m.Title.Contains(nameMovie))
                .ToList();

            if(res != null)
            {
                foreach (var item in res)
                {
                    item.Genres = _genreRepository.GetAllByIdMovie(item.Id);
                    item.Casts = _castRepository.GetAllCastByMovieId(item.Id);
                }
            }

            return res;
        }

        public  List<MovieDTO> GetAll()
        {
            var res = _context.Movies.Select(m => _mapper.Map<MovieDTO>(m)).ToList();

            foreach (var item in res)
            {
                item.Genres = _genreRepository.GetAllByIdMovie(item.Id);
                item.Casts = _castRepository.GetAllCastByMovieId(item.Id);
            }
            return res;
        }

        public MovieUpdate Update(MovieUpdate movie)
        {
            try
            {
                var res = _context.Movies.FirstOrDefault(m => m.Id.Equals(movie.Id));

                if(res == null)
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
            if(res == null)
            {
                return null;
            }
            _context.Movies.Remove(res);
            _context.SaveChanges();
            return res;
        }
    }
}
