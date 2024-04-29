using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Repositories;
using MovieAPI.Result;

namespace MovieAPI.Test.Repositories
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private MovieContext _context;
        private PersonRepository _repository;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _context = DBContext.GetInstance();

            _repository = new PersonRepository(_context);

            // Sample data
            List<Person> people = new List<Person>()
            {
                new() { Id = 1, Name= "Aaron Moten", Nationality = "USA" },
                new() { Id = 2, Name= "Walton Goggins", Nationality = "USA" },
                new() { Id = 3, Name= "Elle Vertes", Nationality = "USA" },
            };

            await _context.AddRangeAsync(people);
            _context.SaveChanges();
        }


        [Test]
        public async Task Get_All()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.That(result.Count, Is.GreaterThan(1));
        }

        #region Add Person

        [Test]
        public async Task Add_Person_Should_Return_Success()
        {
            // Arrange
            var person = new PersonAdd
            {
                Name = "Ella Purnell",
                Born = DateOnly.FromDateTime(DateTime.Now),
                Nationality = "UK"
            };

            // Act
            var result = await _repository.AddAsync(person);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.IsFailure, Is.False);
            });
        }

        [Test]
        public async Task Add_Missing_Data_Should_Return_Failure()
        {
            // Arrange
            var person = new PersonAdd
            {
                Name = "",
                Nationality = ""
            };

            // Act
            var result = await _repository.AddAsync(person);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error.code, Is.EqualTo(Errors.Code.Missing.ToString()));

            });
        }

        #endregion

        #region Remove Person
        [Test]
        [TestCase("spiderman")]
        public async Task Remove_Should_Return_False(string name)
        {
            // Act
            var result = await _repository.RemoveAsync(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.IsFailure, Is.True);
            });
        }

        [Test]
        [TestCase("Walton Goggins")]
        public async Task Remove_Should_Return_True(string name)
        {
            // Act
            var result = await _repository.RemoveAsync(name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.IsFailure, Is.False);
            });
        }

        #endregion

        #region Update

        [Test]
        public async Task Update_Should_Return_True()
        {
            // Arrange
            var person = new PersonUpdate
            {
                Id = 1,
                Name = "Spider Man",
                Born = DateOnly.FromDateTime(DateTime.Now),
                Nationality = "UK"
            };

            // Act
            var result = await _repository.UpdateAsync(person);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.IsFailure, Is.False);
            });
        }

        #endregion
    }
}