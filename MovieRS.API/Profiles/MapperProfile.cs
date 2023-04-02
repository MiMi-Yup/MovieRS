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

            CreateMap<TMDbLib.Objects.General.ImageData, ImageDataDto>();

            CreateMap<TMDbLib.Objects.General.ImagesWithId, ImageDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>, SearchContainerDto<MovieDto>>();

            CreateMap<TMDbLib.Objects.Reviews.AuthorDetails, UserDto>()
                .ForMember(item => item.Username, options => options.MapFrom(item => item.Username))
                .ForMember(item => item.Id, options => options.MapFrom(item => 0));

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>, SeachContainerWithIdDto<ReviewDto>>();

            CreateMap<TMDbLib.Objects.Collections.Collection, CollectionMovieDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>, SearchContainerWithDataRangeDto<MovieDto>>()
                .ForMember(item => item.StartDate, options => options.MapFrom(item => item.Dates.Minimum))
                .ForMember(item => item.EndDate, options => options.MapFrom(item => item.Dates.Maximum));

            CreateMap<TMDbLib.Objects.People.Person, PersonDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>, SearchContainerDto<PersonDto>>();

            CreateMap<TMDbLib.Objects.People.MovieRoleExtension, MovieRoleActDto>();
        }
    }
}
