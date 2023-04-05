using AutoMapper;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.User;
using MovieRS.API.Models;

namespace MovieRS.API.Profiles
{
    public class MapperProfile : Profile
    {
        private string PrefixImage(string path) => $"https://movie-rs.azurewebsites.net/api/image{path}";
        public MapperProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>()
                .ForMember(item => item.Country, options => options.MapFrom(item => item.Country == null ? null : item.Country.Name));

            CreateMap<TMDbLib.Objects.Search.SearchCollection, SearchCollectionDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.Movies.Movie, MovieDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.General.ImageData, ImageDataDto>()
                .ForMember(item => item.FilePath, options => options.MapFrom(item => PrefixImage(item.FilePath)));

            CreateMap<TMDbLib.Objects.General.ImagesWithId, ImageDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>, SearchContainerDto<MovieDto>>();

            CreateMap<TMDbLib.Objects.Reviews.AuthorDetails, UserDto>()
                .ForMember(item => item.Username, options => options.MapFrom(item => item.Username))
                .ForMember(item => item.Id, options => options.MapFrom(item => 0))
                //Chỗ này chưa tìm ra cách
                .ForMember(item => item.Country, options => options.MapFrom(item => "US"));

            CreateMap<TMDbLib.Objects.Reviews.ReviewBase, ReviewDto>()
                .ForMember(item => item.Rating, options => options.MapFrom(item => double.Parse(item.AuthorDetails.Rating)));

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>, SeachContainerWithIdDto<ReviewDto>>();

            CreateMap<TMDbLib.Objects.Collections.Collection, CollectionMovieDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>, SearchContainerWithDataRangeDto<MovieDto>>()
                .ForMember(item => item.StartDate, options => options.MapFrom(item => item.Dates.Minimum))
                .ForMember(item => item.EndDate, options => options.MapFrom(item => item.Dates.Maximum));

            CreateMap<TMDbLib.Objects.People.Person, PersonDto>()
                .ForMember(item => item.ProfilePath, options => options.MapFrom(item => PrefixImage(item.ProfilePath)));

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>, SearchContainerDto<PersonDto>>();

            CreateMap<TMDbLib.Objects.People.MovieRoleExtension, MovieRoleActDto>();

            CreateMap<TMDbLib.Objects.Movies.CastExtension, CastDto>();

            CreateMap<TMDbLib.Objects.Movies.CreditsExtension, CreditsDto>();

            CreateMap<TMDbLib.Objects.People.MovieCreditsExtension, MovieCreditsDto>();

            CreateMap<TMDbLib.Objects.General.Video, VideoDto>();

            CreateMap<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>, ResultContainerDto<VideoDto>>();
        }
    }
}
