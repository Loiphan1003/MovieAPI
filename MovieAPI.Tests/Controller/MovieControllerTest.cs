using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieAPI.Controllers;
using MovieAPI.Entities;
using MovieAPI.Services;

namespace MovieAPI.Tests.Controller
{
    public class MovieControllerTest
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieControllerTest()
        {
            _movieRepository = A.Fake<IMovieRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void GetAll_Should_Return_OK()
        {
            // Arrange
            var queryObject = new QueryObject
            {
                Search = "null",
                SortBy = "null",
                Page = 0,
                PageSize = 0
            };

            var mockMovieRepository = new Mock<IMovieRepository>();
            var controller = new MovieController(mockMovieRepository.Object);

            // Mock data
            var movies = new List<MovieDTO>();

            movies.Add(new MovieDTO
            {
                Id = Guid.Parse("c0f36aad-e7a7-471d-9e6b-a46a138b3485"),
                Title = "Spider-Man: Across the Spider-Verse",
                Budget = 150000000,
                DateRelease = DateTime.Parse("2023-06-02T00:00:00"),
                Runtime = "2h 20m",
                IMDbRate = 8,
            });

            movies.Add(new MovieDTO
            {
                Id = Guid.Parse("c0f36aad-e7a7-471d-9e6b-a46a138b3486"),
                Title = "Spider-Man: No Way Home",
                Budget = 150000000,
                DateRelease = DateTime.Parse("2023-06-02T00:00:00"),
                Runtime = "2h 20m",
                IMDbRate = 8,
            });

            movies.Add(new MovieDTO
            {
                Id = Guid.Parse("c0f36aad-e7a7-471d-9e6b-a46a138b3487"),
                Title = "Car",
                Budget = 150000000,
                DateRelease = DateTime.Parse("2023-06-02T00:00:00"),
                Runtime = "2h 20m",
                IMDbRate = 8,
            });

            mockMovieRepository.Setup(repo => repo.GetAll(It.IsAny<QueryObject>())).Returns(movies);

            // Act
            var result = controller.GetAll(new QueryObject());

            // Assert

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var list = okResult.Value as List<MovieDTO>;

            Assert.True(list.Count() > 0);
        }
    }
}