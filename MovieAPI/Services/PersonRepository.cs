using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MovieContext _context;
        private readonly StoreProcedure _storeProcedure;
        private readonly IMapper _mapper;

        public PersonRepository(MovieContext context, IMapper mapper, StoreProcedure storeProcedure)
        {
            _context = context;
            _mapper = mapper;
            _storeProcedure = storeProcedure;
        }

        public Person Add(PersonVM person)
        {
            Person newPerson = new Person
            {
                Id = Guid.NewGuid(),
                Name = person.Name,
                Born = person.Born,
                Gender = person.Gender,
            };

            _context.Persons.Add(newPerson);
            _context.SaveChanges();
            return newPerson;
        }

        public List<PersonDTO> GetAll()
        {
            return _context.Persons.Select(p => _mapper.Map<PersonDTO>(p)).ToList();
        }
    }
}
