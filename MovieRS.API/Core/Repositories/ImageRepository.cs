using MovieRS.API.Core.Contracts;

namespace MovieRS.API.Core.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private ITMDb _tmdb;

        public ImageRepository(ITMDb tmdb)
        {
            _tmdb = tmdb;
            _tmdb.Client.GetConfigAsync().Wait();
        }

        public Task<byte[]> GetImage(string path, string size = "original", bool useSsl = true)
        {
            return _tmdb.Client.GetImageBytesAsync(size, path, useSsl);
        }
    }
}
