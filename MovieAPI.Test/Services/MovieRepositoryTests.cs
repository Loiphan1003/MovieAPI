using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using MovieAPI.Controllers;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Test.Services
{
    [TestFixture]
    public class MovieRepositoryTests
    {

        private MovieContext GetDBContext ()
        {
            var options = new DbContextOptionsBuilder<MovieContext>()
               .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
               .Options;

            var context = new MovieContext(options);
            return context;
        }

        [Test]
        public void GetAllMovies_ReturnsFilteredSearch()
        {
            // Arrange
            var context = GetDBContext();

            var queryObject = new QueryObject
            {
                Search = "Spider",
                SortBy = "",
                Page = 0,
                PageSize = 0
            };

            var moviesData = new List<Movie>
            {
                new Movie { Title = "Spider-Man: No Way Home", IMDbRate = 9, Budget = 200000000 },
                new Movie { Title = "Spider-Man: Across the Spider-Verse", IMDbRate = 8, Budget = 150000000 },
                new Movie { Title = "Car", IMDbRate = 7, Budget = 100000000 }
            };

            context.Movies.AddRange(moviesData);
            context.SaveChanges();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var repository = new MovieRepository(context, mapper);

            // Act
            var result = repository.GetAll(queryObject);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2)); // Assuming 2 movies match the search criteria and are sorted by IMDbRate in descending order
        }

        [Test]
        public void GetAllMoview_ReturnsWithFilterredSearchIsNullOrEmpty()
        {
            // Arrange
            var context = GetDBContext();

            var queryObject = new QueryObject
            {
                Search = "",
                SortBy = "",
                Page = 0,
                PageSize = 0
            };


            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var repository = new MovieRepository(context, mapper);


            // Act
            var result = repository.GetAll(queryObject);


            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void AddMovie_ShouldSuccess()
        {
            // Arrange
            var context = GetDBContext();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var repository = new MovieRepository(context, mapper);

            MovieVM movieVM = new MovieVM
            {
                Title = "Iron-Man",
                IMDbRate = 7,
                Budget = 100000000
            };

            // Act
            var result = repository.Add(movieVM);
            var movie = context.Movies.FirstOrDefault(m => m.Title.Equals("Iron-Man"));

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo("200"));
            Assert.That(movie.Title, Is.EqualTo("Iron-Man"));
        }

        [Test]
        public void RemovieMovie_ShoouldSuccess()
        {
            // Arrange
            var context = GetDBContext();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Movie, MovieDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var repository = new MovieRepository(context, mapper);

            // Act
            var movie = context.Movies.FirstOrDefault(m => m.Title.Equals("Iron-Man"));
            var result = repository.Delete(movie.Id);
            var movies = context.Movies.ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo("200"));
            Assert.That(movies.Count, Is.EqualTo(3));
        }
    }
}
