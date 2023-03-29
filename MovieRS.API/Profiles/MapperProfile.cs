using AutoMapper;
using Microsoft.Extensions.Hosting;
using MovieRS.API.Dtos.User;
using MovieRS.API.Models;
using Newtonsoft.Json;

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
        }
    }
}
