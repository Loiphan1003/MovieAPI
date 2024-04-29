using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Repositories;

namespace MovieAPI.Test.Repositories
{
    [TestFixture]
    public class GenreRepositoryTests
    {
        private MovieContext _context;
        private GenreRepository _repository;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _context = DBContext.GetInstance();

            _repository = new GenreRepository(_context);


            // Add genre for test
            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        [Test]
        [TestCase("Animation")]
        public async Task Insert_Should_Return_True(string name)
        {
            // Act
            var result = await _repository.InsertAsync(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Message, Is.Not.Empty);
                Assert.That(result.Data, Is.TypeOf<Genre>());
            });
        }

        [Test]
        [TestCase("Action")]
        public async Task Insert_Duplacate_Name_Should_Return_False(string name)
        {
            // Act
            var result = await _repository.InsertAsync(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Message, Is.Empty);
                Assert.That(result.Error.description, Is.Not.Exist);
                Assert.That(result.Error.code, Is.EqualTo(Error.Code.AlreadyCreate.ToString()));
            });
        }

        [Test]
        [TestCase(1)]
        public async Task Remove_Should_Return_True(int id)
        {
            // Act
            var result = await _repository.RemoveAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Error.code, Is.EqualTo(string.Empty));
                Assert.That(result.Error.description, Is.EqualTo(string.Empty));
            });
        }


        [Test]
        public async Task Remove_Should_Return_Not_Found()
        {
            // Set up
            var genres = _repository.GetAllAsync();

            // Act
            var result = await _repository.RemoveAsync(genres.Count + 1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.code, Is.EqualTo(Error.Code.NotFound.ToString()));
                Assert.That(result.Error.description, Is.Not.EqualTo(string.Empty));
            });
        }

        [Test]
        public async Task Update_Should_Return_True()
        {
            // Set up
            var genre = new GenreDTO
            {
                Id = 2,
                Name = "Adventure"
            };

            // Act
            var result = await _repository.UpdateAsync(genre);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.TypeOf<Genre>());
                Assert.That(result.Message, Is.Not.EqualTo(string.Empty));
            });
        }

        [Test]
        public async Task Update_Should_Return_Not_Found()
        {
            // Set up
            var genres = _repository.GetAllAsync();

            var genre = new GenreDTO
            {
                Id = 10,
                Name = "Adventure"
            };

            // Act
            var result = await _repository.UpdateAsync(genre);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.code, Is.EqualTo(Error.Code.NotFound.ToString()));
                Assert.That(result.Error.description, Is.Not.EqualTo(string.Empty));
            });

        }
    }
}
