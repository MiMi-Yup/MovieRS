using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Models;
using TMDbLib.Objects.Collections;

namespace MovieRS.API.Core.Repositories
{
    public class CollectionRepository : GenericRepository<Movie>, ICollectionRepository
    {
        private readonly ITMDb _tmdb;
        private readonly IMovieRepository _movieRepository;
        public CollectionRepository(MovieRsContext context, ILogger logger, IMapper mapper, ITMDb tmdb, IMovieRepository movieRepository) : base(context, logger, mapper)
        {
            _tmdb = tmdb;
            _movieRepository = movieRepository;
        }

        public async Task<TMDbLib.Objects.Collections.CollectionExtension> GetCollection(int id)
        {
            var collection = await _tmdb.Client.GetCollectionAsync(id);
            var convertCollection = collection.Convert();
            convertCollection.Parts = (await Task.WhenAll(collection.Parts.Select(item => _movieRepository.GetMovieBy3rd(item.Id)))).ToList();
            return convertCollection;
        }

        public Task<TMDbLib.Objects.General.SearchContainer<TMDbLib.Objects.Search.SearchCollection>> SearchCollection(string query)
        {
            return _tmdb.Client.SearchCollectionAsync(query);
        }
    }
}
