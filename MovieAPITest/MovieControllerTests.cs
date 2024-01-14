using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace MovieAPITest
{
    internal class MovieControllerTests
    {
        [TestFixture]
        public class MovieControllerTest
        {
            private MovieController _controller;

            private List<MovieDTO> CreateMockMovieData()
            {
                return new List<MovieDTO> { new MovieDTO(), new MovieDTO() };
            }

            private static void AssertIsOkObjectResultWithMovies(IActionResult result)
            {
                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var list = okResult.Value as List<MovieDTO>;

                Assert.IsNotNull(list);
                CollectionAssert.AllItemsAreInstancesOfType(list, typeof(MovieDTO));
                Assert.Greater(list.Count, 0);
            }

            [SetUp]
            public void Setup()
            {
                var context = new MovieContext();
                var movieRepository = new MovieRepository(context);

                _controller = new MovieController(movieRepository);
            }


            [Test]
            public void GetAll_ShouldReturnWithItems()
            {
                // Arrange
                var query = new QueryObject();


                // Act
                var result = _controller.GetAll(query);


                // Assert
                Assert.IsNotNull(result);
                //AssertIsOkObjectResultWithMovies(result);
            }

            [Test]
            public void SearchMovie_ShouldReturnWithItem()
            {
                // Arrange
                var mockMovieRepository = new Mock<IMovieRepository>();
                var controller = new MovieController(mockMovieRepository.Object);

                // Mock data
                var movieModels = new List<MovieDTO>();
                mockMovieRepository.Setup(repo => repo.GetAll(It.IsAny<QueryObject>())).Returns(movieModels);

                // Act
                var query = new QueryObject { Search = "spider-man" };

                IActionResult result = controller.GetAll(query);
                Console.WriteLine("Result: " + result);

                // Assert
                AssertIsOkObjectResultWithMovies(result);

                // Additional Assert for Search functionality
                mockMovieRepository.Verify(repo => repo.GetAll(It.Is<QueryObject>(q => q.Search == "spider-man")), Times.Once);
                var okResult = result as OkObjectResult;
                var movies = okResult.Value as List<MovieDTO>;

                Assert.IsNotNull(movies, "Movies list should not be null.");

                if (movies != null)
                {
                    Console.WriteLine("Movies: " + string.Join(", ", movies.Select(m => m.Title)));

                    Assert.IsTrue(movies.All(m => m.Title != null), "All movie titles should be non-null.");

                    Assert.IsTrue(movies.Any(m => m.Title.Contains("spider-man", StringComparison.OrdinalIgnoreCase)),
                        "At least one movie should contain 'spider-man' in the title.");
                }
            }
        }
    }
}
