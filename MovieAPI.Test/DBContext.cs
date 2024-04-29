using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;

namespace MovieAPI.Test
{
    public sealed class DBContext
    {
        private static MovieContext? _instance;
        private static readonly object _lock = new object();

        private DBContext()
        {

        }

        public static MovieContext GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    var options = new DbContextOptionsBuilder<MovieContext>()
                        .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                        .Options;

                    _instance = new MovieContext(options);
                }

                return _instance;
            }
        }
    }
}