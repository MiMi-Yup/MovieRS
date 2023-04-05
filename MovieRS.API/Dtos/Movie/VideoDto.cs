using Newtonsoft.Json;

namespace MovieRS.API.Dtos.Movie
{
    public class VideoDto
    {
        public string? Id { get; set; }

        /// <summary>
        /// A language code, e.g. en
        /// </summary>
        public string? Iso_639_1 { get; set; }
        public string? Key { get; set; }
        public string? Site { get; set; }
        public string? Type { get; set; }
    }
}
