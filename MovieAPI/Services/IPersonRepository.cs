using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IPersonRepository
    {
        Person Add(PersonVM person);
        List<PersonDTO> GetAll();

    }
}
