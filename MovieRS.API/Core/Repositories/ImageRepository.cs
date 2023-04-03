using MovieRS.API.Core.Contracts;
using MovieRS.API.Error;

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

        public async Task<byte[]> GetImage(string path, string size = "original", bool useSsl = true)
        {
            try
            {
                byte[] image = await _tmdb.Client.GetImageBytesAsync(size, path, useSsl);
                return image;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(ex.Message, System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
