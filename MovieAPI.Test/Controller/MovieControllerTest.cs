using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieAPI.Controllers;
using MovieAPI.Entities;
using MovieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Test.controller
{
    internal class MovieControllerTest
    {
        [TestFixture]
        public class MovieControllerTests
        {
            [Test]
            public void GetAll_ShouldReturnListWithItems()
            {
                // Arrange
                var query = new QueryObject
                {
                    Search = "",
                    SortBy = "",
                    Page = 0,
                    PageSize = 0
                };


                var mockMovieRepository = new Mock<IMovieRepository>();
                var controller = new MovieController(mockMovieRepository.Object);

                // Mock data
                var movies = new List<MovieDTO>
                {
                    new MovieDTO { Title = "Spider-Man: Across the Spider-Verse" },
                    new MovieDTO { Title = "Spider-Man: No Way Home" },
                    new MovieDTO { Title = "Car" }
                };

                mockMovieRepository.Setup(repo => repo.GetAll(It.IsAny<QueryObject>())).Returns(movies);

                // Act
                var result = controller.GetAll(query);

                // Assert
                var okResult = result as OkObjectResult;

                var list = okResult.Value as List<MovieDTO>;
                Assert.IsNotNull(list);

                Assert.Greater(list.Count, 0);
            }

            [Test]
            public void MovieSearch_ShouldReturnWithItems()
            {
                // Arrange
                var queryObject = new QueryObject { Search = "Sample" };
                var expectedMovies = new List<MovieDTO>
                {
                    new MovieDTO { Title = "Spider-Man: Across the Spider-Verse" },
                    new MovieDTO { Title = "Spider-Man: No Way Home" },
                    new MovieDTO { Title = "Car" }
                };

                var mockMovieService = new Mock<IMovieRepository>();

                // Setup mock for GetAll with specific Search value
                mockMovieService.Setup(service => service.GetAll(It.Is<QueryObject>(q => q.Search == "Sample")))
                    .Returns(expectedMovies);

                var controller = new MovieController(mockMovieService.Object);

                // Act
                var result = controller.GetAll(queryObject) as OkObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Value, Is.EqualTo(expectedMovies));

                // Verify that GetAll was called with the correct QueryObject
                mockMovieService.Verify(service => service.GetAll(It.Is<QueryObject>(q => q.Search == "Sample")), Times.Once);
            }
        }
    }
}
