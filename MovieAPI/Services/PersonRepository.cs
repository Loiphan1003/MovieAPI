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

        public Person Delete(Guid id)
        {
            var res = _context.Persons.FirstOrDefault(p => p.Id.Equals(id));

            if (res == null)
            {
                return null;
            }

            _context.Persons.Remove(res);
            _context.SaveChanges();
            return res;
        }

        public List<PersonDTO> GetAll(QueryObject queryObject)
        {
            var query = _context.Persons.AsQueryable();

            #region Search Name
            if (!string.IsNullOrEmpty(queryObject.Search))
            {
                query = query.Where(p => p.Name.Contains(queryObject.Search));
            }
            #endregion

            #region Sort BY
            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                switch (queryObject.SortBy)
                {
                    case "name":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                }
            }
            #endregion

            #region Paging
            if (queryObject.Page > 0 && queryObject.PageSize > 0)
            {
                query = query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
            #endregion

            var persons = query.Select(p => _mapper.Map<PersonDTO>(p)).ToList();

            return persons;
        }

        public List<PersonDTO> GetByName(string name)
        {
            var res = _context.Persons
                .Select(p => _mapper.Map<PersonDTO>(p))
                .AsEnumerable()
                .Where(p => p.Name.Contains(name))
                .ToList();
            return res;
        }

        public PersonDTO Update(PersonDTO person)
        {
            var res = _context.Persons.FirstOrDefault(p => p.Id.Equals(person.Id));

            if (res == null)
            {
                return null;
            }

            res.Name = person.Name;
            res.Born = person.Born;
            res.Gender = person.Gender;
            _context.Persons.Update(res);
            _context.SaveChanges();
            return person;
        }
    }
}
