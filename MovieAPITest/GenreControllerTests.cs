using Microsoft.AspNetCore.Http;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieAPITest
{
    internal class GenreControllerTests
    {
        [TestFixture]
        public class GenreControllerTest
        {
            [Test]
            public void GetAll_ShouldReturnListWithItems()
            {
                // Arrange
                var mockGenreRepository = new Mock<IGenreRepository>();
                var controller = new GenreController(mockGenreRepository.Object);

                // Mock data
                var genreModels = new List<GenreDTO> { new GenreDTO(), new GenreDTO() };

                mockGenreRepository.Setup(repo => repo.GetAll(It.IsAny<QueryObject>())).Returns(genreModels);

                // Act
                var result = controller.GetAll(new QueryObject());

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var list = okResult.Value as List<GenreDTO>;
                Assert.IsNotNull(list);

                Assert.Greater(list.Count, 0);
            }
        }
    }
}
