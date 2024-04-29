using Mapster;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.Entities;
using MovieAPI.Entities.ViewModels;
using MovieAPI.Result;

namespace MovieAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MovieContext _context;

        public PersonRepository(MovieContext context)
        {
            _context = context;
        }


        // <summary>
        // Add a new person asynchronously.
        // </summary>
        // <param name="model">The data of the person to be added.</param>
        // <returns>
        // A task representing the asynchronous operation.
        // The task result contains a Result<Person> indicating the success or failure of the operation,
        // along with any relevant error message or the added person if successful.
        // </returns>
        public async Task<Result<Person>> AddAsync(PersonAdd model)
        {
            // Ensure the provided data is valid
            if (model.Validate() == false)
            {
                return Result<Person>.Failure(Errors.MissingData("Missing data require"));
            }

            try
            {
                // Check if a person with the same name already exists
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Name.Equals(model.Name));

                if (person != null)
                {
                    return Result<Person>.Failure(Errors.AlreadyCreate($"Person with name {model.Name} has been added"));
                }

                // Create a new person entity from the model
                person = model.Adapt<Person>();

                // Add the new person to the context and save changes
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();

                // Return success result with the added person
                return Result<Person>.Success("Add successful", person);
            }
            catch (Exception ex)
            {
                return Result<Person>.Failure(Errors.InternalError($"Error: {ex.Message}"));
            }

        }

        public async Task<List<Person>> GetAllAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }


        // <summary>
        // Remove a person asynchronously
        // </summary>
        // <param name="name">The name of person to be removed.</param>
        public async Task<Result<Person>> RemoveAsync(string name)
        {
            // Ensure name is not null or empty
            if (string.IsNullOrEmpty(name))
            {
                return Result<Person>.Failure(Errors.MissingData("name is not null or empty"));
            }

            try
            {
                // Check if a person with name is not null
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.Name.Equals(name));
                if (person == null)
                {
                    return Result<Person>.Failure(Errors.NotFound("Person is not found"));
                }

                // Remove the person to the context and save change
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();

                // Return success
                return Result<Person>.Success("Remove successful!", default!);
            }
            catch (Exception ex)
            {
                return Result<Person>.Failure(Errors.InternalError(ex.ToString()));
            }

        }


        // <summary>
        // Update a person asynchronously
        // </summary>
        // <param name="model">The data of the person to be updated </param>
        public async Task<Result<Person>> UpdateAsync(PersonUpdate model)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == model.Id);
            if (person == null)
            {
                return Result<Person>.Failure(Errors.NotFound("Person is not found"));
            }

            if (person.Name.Equals(model.Name))
            {
                return Result<Person>.Failure(Errors.AlreadyCreate($"The person with name {model.Name} has been already exists"));
            }

            person.Name = model.Name;
            person.Nationality = model.Nationality;

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            return Result<Person>.Success("Update successful", person);
        }
    }
}