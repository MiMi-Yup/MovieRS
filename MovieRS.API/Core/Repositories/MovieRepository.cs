﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos.Movie;
using MovieRS.API.Error;
using MovieRS.API.Models;
using TMDbLib.Objects.Movies;

namespace MovieRS.API.Core.Repositories
{
    public class MovieRepository : GenericRepository<MovieRS.API.Models.Movie>, IMovieRepository
    {
        private readonly ITMDb _tmdb;
        private readonly IReviewRepository _reviewRepository;

        public MovieRepository(MovieRsContext context, ILogger logger, IMapper mapper, ITMDb tmdb, IReviewRepository reviewRepository) : base(context, logger, mapper)
        {
            _tmdb = tmdb;
            _reviewRepository = reviewRepository;
        }

        public async Task<TMDbLib.Objects.General.ImagesWithId> GetImages(int id, int take = 0)
        {
            _tmdb.ChangeLanguage(null);
            TMDbLib.Objects.General.ImagesWithId image = await _tmdb.Client.GetMovieImagesAsync(id);
            _tmdb.ChangeLanguage("vi");
            return new TMDbLib.Objects.General.ImagesWithId
            {
                Id = image.Id,
                Backdrops = image.Backdrops.Take(take > 0 ? take : image.Backdrops.Count).ToList(),
                Posters = image.Posters.Take(take > 0 ? take : image.Posters.Count).ToList(),
                Logos = image.Logos.Take(take > 0 ? take : image.Logos.Count).ToList()
            };
        }

        public async Task<TMDbLib.Objects.Movies.Movie> GetMovie(int id)
        {
            try
            {
                return await _tmdb.Client.GetMovieAsync(id);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>> GetVideos(int id)
        {
            try
            {
                _tmdb.ChangeLanguage("en");
                Task<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>> videoEn = _tmdb.Client.GetMovieVideosAsync(id);
                _tmdb.ChangeLanguage("vi");
                Task<TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>> videoVn = _tmdb.Client.GetMovieVideosAsync(id);
                TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>[] videos = await Task.WhenAll(videoEn, videoVn);
                List<TMDbLib.Objects.General.Video> filteredVideo = videos
                    .Select(item => item.Results)
                    .SelectMany(item => item)
                    .DistinctBy(item => item.Id)
                    .Where(item => item.Site == "YouTube" && item.Type == "Trailer")
                    .ToList();
                if (!filteredVideo.Any())
                    filteredVideo.Add(new TMDbLib.Objects.General.Video
                    {
                        Iso_639_1 = "en",
                        Site = "Custom",
                        Key = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                        Type = "Trailer"
                    });

                //check ngrok and pass link
                filteredVideo.Add(new TMDbLib.Objects.General.Video
                {
                    Iso_639_1 = "vi",
                    Site = "Custom",
                    Type = "Full",
                    Key = /*"<PASS LINK TO HERE>"*/"http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"
                });

                return new TMDbLib.Objects.General.ResultContainer<TMDbLib.Objects.General.Video>
                {
                    Id = id,
                    Results = filteredVideo
                };
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.NotFound);
            }
        }

        public async Task<TMDbLib.Objects.Movies.CreditsExtension?> GetActors(IPersonRepository repository, int id, int take = 0)
        {
            TMDbLib.Objects.Movies.Credits credits = await _tmdb.Client.GetMovieCreditsAsync(id);
            if (credits != null)
            {
                return new TMDbLib.Objects.Movies.CreditsExtension
                {
                    Id = credits.Id,
                    Cast = (await Task.WhenAll(credits.Cast.Take(take > 0 ? take : credits.Cast.Count).Select(async item =>
                    {
                        TMDbLib.Objects.Movies.CastExtension cast = item.Convert();
                        cast.Person = await repository.GetPerson(item.Id);
                        return cast;
                    }))).ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetNowPlaying(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieNowPlayingListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Take(take > 0 ? take : movie.Results.Count).Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
                {
                    Dates = movie.Dates,
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = result.ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> NewReview(NewReviewDto newReview)
        {
            if (!this.Find(item => item.Id == newReview.Id).Any() && await this.Add(new Models.Movie { Id = newReview.Id }))
            {
                await _reviewRepository.NewReviews(newReview);
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetPopular(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMoviePopularListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Take(take > 0 ? take : movie.Results.Count).Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = result.ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetRecommendation(int id, int page = 1, int take = 0)
        {
            var recommendation = await _tmdb.Client.GetMovieRecommendationsAsync(id, page);
            if (recommendation != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(recommendation.Results.Take(take > 0 ? take : recommendation.Results.Count).Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = recommendation.Page,
                    TotalPages = recommendation.TotalPages,
                    TotalResults = recommendation.TotalResults,
                    Results = result.ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>> GetReview(int id, int page = 1, int take = 0)
        {
            _tmdb.ChangeLanguage("en");
            TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase> review = await _tmdb.Client.GetMovieReviewsAsync(id, page: page < 1 ? 1 : page);
            _tmdb.ChangeLanguage("vi");
            return new TMDbLib.Objects.General.SearchContainerWithId<TMDbLib.Objects.Reviews.ReviewBase>
            {
                Id = review.Id,
                Page = review.Page,
                TotalPages = review.TotalPages,
                TotalResults = review.TotalResults,
                Results = review.Results.Take(take > 0 ? take : review.Results.Count).ToList()
            };
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> GetTopRated(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieTopRatedListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Take(take > 0 ? take : movie.Results.Count).Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
                {
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = result.ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>?> GetUpComming(int page = 1, int take = 0)
        {
            var movie = await _tmdb.Client.GetMovieUpcomingListAsync(page: page < 1 ? 1 : page);
            if (movie != null)
            {
                TMDbLib.Objects.Movies.Movie[] result = await Task.WhenAll(movie.Results.Take(take > 0 ? take : movie.Results.Count).Select(item => GetMovie(item.Id)));
                return new TMDbLib.Objects.General.SearchContainerWithDates<TMDbLib.Objects.Movies.Movie>
                {
                    Dates = movie.Dates,
                    Page = movie.Page,
                    TotalPages = movie.TotalPages,
                    TotalResults = movie.TotalResults,
                    Results = result.ToList()
                };
            }
            return null;
        }

        public async Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>?> SearchMovie(string query, int year = 0, string? region = null, int primaryReleaseYear = 0, int page = 1)
        {
            var movie = await _tmdb.Client.SearchMovieAsync(query, page: page < 1 ? 1 : page, year: year, region: region, primaryReleaseYear: primaryReleaseYear);
            return movie != null ? new TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Movies.Movie>
            {
                Page = movie.Page,
                TotalPages = movie.TotalPages,
                TotalResults = movie.TotalResults,
                Results = (await Task.WhenAll(movie.Results.Select(item => GetMovie(item.Id)))).ToList()
            } : null;
        }

        public Task<IEnumerable<Models.Movie>> GetLocalVideos()
        {
            return Task.FromResult<IEnumerable<Models.Movie>>(dbSet.AsNoTracking());
        }

        public async Task<bool> NewVideo(TMDbLib.Objects.Movies.Movie newMovie)
        {
            if (dbSet.FirstOrDefault(item => item.IdTmdb == newMovie.Id) == null)
            {
                Models.Movie movie = new Models.Movie { IdTmdb = newMovie.Id, IdImdb = int.Parse(newMovie.ImdbId.Substring(2)), YearRelease = (short?)(newMovie.ReleaseDate.HasValue ? newMovie.ReleaseDate.Value.Year : DateTime.Now.Year) };
                try
                {
                    if (await this.Add(movie))
                    {
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, System.Net.HttpStatusCode.Conflict);
                }
            }
            return true;
        }
    }
}
