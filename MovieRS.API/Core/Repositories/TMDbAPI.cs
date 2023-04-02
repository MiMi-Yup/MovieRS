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
            ChangeToVN();
        }

        public void ChangeToEN()
        {
            Client.DefaultLanguage = "en";
            Client.DefaultCountry = "US";
        }

        public void ChangeToVN()
        {
            Client.DefaultLanguage = "vi";
            Client.DefaultCountry = "VN";
        }
    }
}
