using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public interface IPersonRepository
    {
        Person Add(PersonVM person);
        List<PersonDTO> GetAll(QueryObject query);
        List<PersonDTO> GetByName(string name);
        PersonDTO Update(PersonDTO person);
        Person Delete(Guid id);
    }
}
