using MovieRS.API.Core.Contracts;
using TMDbLib.Client;

namespace MovieRS.API.Core.Repositories
{
    public class TMDbAPI : ITMDb
    {
        public TMDbClient Client { get;private set; }
        private readonly IConfiguration _configuration;

        public TMDbAPI(IConfiguration configuration)
        {
            _configuration = configuration;
            Client = new TMDbClient(_configuration["TMDB:API_Key"]);
            ChangeLanguage("vi");
        }

        public void ChangeLanguage(string? lang)
        {
            switch (lang)
            {
                case "en":
                    Client.DefaultLanguage = "en";
                    Client.DefaultCountry = "US";
                    break;
                case "vi":
                    Client.DefaultLanguage = "vi";
                    Client.DefaultCountry = "VN";
                    break;
                default:
                    Client.DefaultLanguage = null;
                    Client.DefaultCountry = null;
                    break;
            }
        }
    }
}
