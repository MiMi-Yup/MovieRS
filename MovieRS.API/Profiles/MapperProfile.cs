﻿using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;
using MovieRS.API.Dtos.Favourite;
using MovieRS.API.Dtos.History;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Dtos.Recommendation;
using MovieRS.API.Dtos.Review;
using MovieRS.API.Dtos.Search;
using MovieRS.API.Dtos.User;
using TMDbLib.Objects.Reviews;

namespace MovieRS.API.Profiles
{
    public class MapperProfile : Profile
    {
        private string PrefixVideoImage(string path) => $"https://movie-rs.azurewebsites.net/api/image{path}";
        private string PrefixCountryImage(string code) => $"https://cdn.jsdelivr.net/npm/country-flag-emoji-json@2.0.0/dist/images/{code}.svg";

        public MapperProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<UserDto, Models.User>();

            CreateMap<Models.User, UserDto>();

            CreateMap<TMDbLib.Objects.Search.SearchCollection, SearchCollectionDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixVideoImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixVideoImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.Movies.Movie, MovieDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixVideoImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixVideoImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.General.ImageData, ImageDataDto>()
                .ForMember(item => item.FilePath, options => options.MapFrom(item => PrefixVideoImage(item.FilePath)));

            CreateMap<TMDbLib.Objects.General.ImagesWithId, ImageDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>, SearchContainerDto<MovieDto>>();

            CreateMap<TMDbLib.Objects.Reviews.AuthorDetails, UserDto>()
                .ForMember(item => item.Username, options => options.MapFrom(item => item.Username))
                .ForMember(item => item.Id, options => options.MapFrom(item => 0))
                //Chỗ này chưa tìm ra cách
                .ForMember(item => item.Country, options => options.MapFrom(item => "US"));

            CreateMap<TMDbLib.Objects.Reviews.ReviewBaseExtension, ReviewDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBaseExtension>, SearchContainerWithIdDto<ReviewDto>>();

            CreateMap<TMDbLib.Objects.Collections.Collection, CollectionMovieDto>()
                .ForMember(item => item.PosterPath, options => options.MapFrom(item => PrefixVideoImage(item.PosterPath)))
                .ForMember(item => item.BackdropPath, options => options.MapFrom(item => PrefixVideoImage(item.BackdropPath)));

            CreateMap<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>, SearchContainerWithDataRangeDto<MovieDto>>()
                .ForMember(item => item.StartDate, options => options.MapFrom(item => item.Dates.Minimum))
                .ForMember(item => item.EndDate, options => options.MapFrom(item => item.Dates.Maximum));

            CreateMap<TMDbLib.Objects.People.Person, PersonDto>()
                .ForMember(item => item.ProfilePath, options => options.MapFrom(item => PrefixVideoImage(item.ProfilePath)));

            CreateMap<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.People.Person>, SearchContainerDto<PersonDto>>();

            CreateMap<TMDbLib.Objects.People.MovieRoleExtension, MovieRoleActDto>();

            CreateMap<TMDbLib.Objects.Movies.CastExtension, CastDto>();

            CreateMap<TMDbLib.Objects.Movies.CreditsExtension, CreditsDto>();

            CreateMap<TMDbLib.Objects.People.MovieCreditsExtension, MovieCreditsDto>();

            CreateMap<TMDbLib.Objects.General.Video, VideoDto>();

            CreateMap<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>, ResultContainerDto<VideoDto>>();

            CreateMap<Recommendation, RecommendationDto>();

            CreateMap<ResultRecommendation, ResultRecommendationDto>();

            CreateMap<AuthorDetailsExtension, UserDto>();

            CreateMap<Models.Country, CountryDto>()
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Code == null ? null : PrefixCountryImage(item.Code)));

            CreateMap<TMDbLib.Objects.General.SearchContainer<Models.Country>, SearchContainerDto<CountryDto>>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Movies.Movie>, SearchContainerWithIdDto<MovieDto>>();

            CreateMap<HistoryMovie, HistoryDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<HistoryMovie>, SearchContainerWithIdDto<HistoryDto>>();

            CreateMap<UserReview, UserReviewDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<UserReview>, SearchContainerWithIdDto<UserReviewDto>>();

            CreateMap<UserFavourite, FavouriteDto>();

            CreateMap<TMDbLib.Objects.General.SearchContainerWithId<UserFavourite>, SearchContainerWithIdDto<FavouriteDto>>();
        }
    }
}
