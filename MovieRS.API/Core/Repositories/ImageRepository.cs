using MovieRS.API.Core.Contracts;
using MovieRS.API.Error;

namespace MovieRS.API.Core.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private ITMDb _tmdb;
        private static readonly string[] widthAccept = new[] { "w154", "w300", "w500", "w780", "w1280", "orignal" };

        public ImageRepository(ITMDb tmdb)
        {
            _tmdb = tmdb;
            _tmdb.Client.GetConfigAsync().Wait();
        }

        public async Task<byte[]> GetImage(string path, string size = "w154", bool useSsl = true)
        {
            if (!widthAccept.Contains(size))
                new ApiException("Invalid request width", System.Net.HttpStatusCode.BadRequest);
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
