using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Controllers;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Repositories;

namespace MovieAPI.Test.Controller
{
    [TestFixture]
    public class GenreControllerTest
    {
        private MovieContext _context;
        private GenreController _controller;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _context = DBContext.GetInstance();

            var repository = A.Fake<IGenreRepository>();

            _controller = new GenreController(repository);

            var genres = new List<Genre>
            {
                new () { Name = "Cartoon"},
                new () { Name = "Anime"}
            };

            _context.Genres.AddRange(genres);
            _context.SaveChanges();
        }

        [Test]
        public void Get_All_Return_OK()
        {
            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        #region Insert

        [Test]
        public async Task Insert_Return_Bad_Request()
        {
            var result = await _controller.Insert("");

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        public async Task Insert_Return_Not_A_String()
        {
            var result = await _controller.Insert("12");

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        #endregion

        #region Remove

        [Test]
        public async Task Remove_Return_Bad_Request()
        {
            // Act
            var result = await _controller.Remove(0);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task Remove_Return_Not_Found()
        {
            // Act
            var result = await _controller.Remove(10);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

        #region Update

        [Test]
        public async Task Update_Return_Bad_Request()
        {
            // set up
            var genre = new GenreDTO
            {
                Id = 0,
                Name = string.Empty
            };

            // Act
            var result = await _controller.Update(genre);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task Update_Return_Not_Found()
        {
            // Set up
            var genres = await _context.Genres.ToListAsync();

            var genre = new GenreDTO
            {
                Id = genres.Count + 1,
                Name = "Anime"
            };

            // Act
            var result = await _controller.Update(genre);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        #endregion

    }
}