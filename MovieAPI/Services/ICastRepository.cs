using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface ICastRepository
    {
        List<Cast> GetAll();
        Cast AddOne(CastVM cast);
    }
}
