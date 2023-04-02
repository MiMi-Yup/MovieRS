using AutoMapper;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>()
                .ForMember(item => item.Country, options => options.MapFrom(item => item.Country == null ? null : item.Country.Name));

            CreateMap<TMDbLib.Objects.Movies.Movie, MovieDto>();
        }
    }
}
