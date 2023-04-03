namespace MovieRS.API.Core.Contracts
{
    public interface IImageRepository
    {
        /// <summary>
        /// Get image from TMDb
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <param name="useSsl"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        Task<byte[]> GetImage(string path, string size = "original", bool useSsl = true);
    }
}
