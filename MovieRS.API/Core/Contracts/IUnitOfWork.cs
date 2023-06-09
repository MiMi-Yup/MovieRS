﻿using MovieRS.API.Models;

namespace MovieRS.API.Core.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ICountryRepository Country { get; }
        IMovieRepository Movie { get; }
        ICollectionRepository CollectionMovie { get; }
        IPersonRepository Person { get; }
        IImageRepository Image { get; }
        IRecommendRepository Recommend { get; }
        IHistoryMovieRepository HistoryMovie { get; }
        IFavouriteRepository Favorite { get; }
        IVideoAPI VideoAPI { get; }
        IReviewRepository Review { get; }
    }
}
