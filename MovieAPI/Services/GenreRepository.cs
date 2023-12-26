﻿using AutoMapper;
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

        public GenreRepository(MovieContext context, StoreProcedure storeProcedure, IMapper mapper)        {
            _context = context;
            _storeProcedure = storeProcedure;
            _mapper = mapper;
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

        public List<GenreDTO> GetAll()
        {
            return _context.Genres.Select(g => _mapper.Map<GenreDTO>(g)).ToList();
        }

        public List<GenreDTO> GetAllByIdMovie(Guid movieId)
        {
            var res = _context.Genres
                    .FromSql(_storeProcedure.FindGenreByMovieId(movieId))
                    .AsEnumerable()
                    .Select(g => _mapper.Map<GenreDTO>(g))
                    .ToList();

            return res;
        }

        public GenreDTO RemoveById(Guid id)
        {
            var res = _context.Genres
                .FirstOrDefault(g => g.Id == id);

            if(res == null )
            {
                return null;
            }
            _context.Genres.Remove(res);
            _context.SaveChanges();

            return new GenreDTO { Id = res.Id, Name = res.Name };
        }
    }
}
