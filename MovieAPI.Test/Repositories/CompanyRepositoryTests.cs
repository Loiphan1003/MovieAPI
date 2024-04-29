using MovieAPI.Data;
using MovieAPI.Repositories;

namespace MovieAPI.Test.Repositories
{
    [TestFixture]
    public class CompanyRepositoryTests
    {
        private MovieContext _context;
        private CompanyRepository _repository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _context = DBContext.GetInstance();

            _repository = new CompanyRepository(_context);

            // Add genre for test
            var company = new Company { Name = "Naughty Dog" };

            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        [Test]
        public void Get_All()
        {
            // Act
            var result = _repository.GetAllAsync();

            // Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        #region Remove

        [Test]
        public void RemoveAsync()
        {

        }

        #endregion
    }
}