using AutoMapper;
using MovieAPI.Data;
using MovieAPI.Entities;

namespace MovieAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<Person, PersonDTO>();
            CreateMap<Cast, CastDTO>();
        }
    }
}
