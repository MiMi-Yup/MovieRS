using AutoMapper;
using MovieRS.API.Core.Contracts;
using MovieRS.API.Dtos;

namespace MovieRS.API.Core.Repositories
{
    public class VideoAPI : GenericRepository<Models.Param>, IVideoAPI
    {
        private const string PARAM = "Video_API_Domain";

        public VideoAPI(Models.MovieRsContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
        {
        }

        public async Task<string> GetLink(int id)
        {
            Models.Param? param = await FindById(PARAM, false);
            if (param == null)
                return string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(param.Value!);
                HttpResponseMessage response = await client.GetAsync($"/status/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string co = await response.Content.ReadAsStringAsync();
                    ApiResponse<StatusVideoDto>? content = await response.Content.ReadFromJsonAsync<ApiResponse<StatusVideoDto>>();
                    if (content != null && content.Data?.Available == true)
                    {
                        return $"{param.Value}/video/{id}";
                    }
                }
            }
            return string.Empty;
        }

        public async Task<bool> UpdateDomain(string domain)
        {
            bool allow = false;
            if (Uri.TryCreate(domain, UriKind.Absolute, out Uri? uri))
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = uri;
                    HttpResponseMessage response = await client.GetAsync("/status");
                    if (response.IsSuccessStatusCode)
                    {
                        ApiResponse<bool>? content = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
                        if (content != null && content.Data)
                        {
                            allow = true;
                        }
                    }
                }
            if (allow)
            {
                Models.Param? param = await dbSet.FindAsync(PARAM);
                if (param != null)
                    param.Value = domain;
                else
                {
                    param = new Models.Param
                    {
                        Id = PARAM,
                        Value = domain
                    };
                    await dbSet.AddAsync(param);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
