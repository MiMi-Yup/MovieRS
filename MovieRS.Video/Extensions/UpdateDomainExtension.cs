using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Timers;

namespace MovieRS.Video.Extensions
{
    public interface IUpdateDomain : IDisposable
    {
        Task<bool> Access(string email, string password);
        string? NgrokUrl { get; }
    }

    public class UpdateDomainExtension : IUpdateDomain
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        private string? _token;
        private string? ObsToken
        {
            get => _token;
            set
            {
                _token = value;
                if (!string.IsNullOrEmpty(_token))
                {
                    Init(null, null);
                }
            }
        }

        private static readonly Regex regex = new Regex(@"url=(.*)");
        private static readonly System.Timers.Timer timer = new System.Timers.Timer();
        private Process? process = null;
        private const int TimeInterval = 2 * 60 * 60 * 1000;
        private readonly ProcessStartInfo startInfo;

        public string? NgrokUrl { get; private set; }

        public UpdateDomainExtension(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<UpdateDomainExtension>();

            startInfo = new ProcessStartInfo
            {
                FileName = "ngrok.exe",
                Arguments = $"http {_configuration.GetValue<int>("StaticPort")} --log=stdout",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            timer.Interval = TimeInterval;
            timer.Elapsed += Init;
        }

        private void Init(object? sender, ElapsedEventArgs e)
        {
            timer.Stop();

            _logger.LogInformation("Ngrok session expired. Create new.");
            process?.Kill();
            process?.Dispose();

            process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
        }

        private async void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;
            Match match = regex.Match(e.Data);
            if (match.Success && match.Groups.Count > 1)
            {
                if (sender is Process own)
                {
                    own.CancelOutputRead();
                    own.OutputDataReceived -= Process_OutputDataReceived;
                }
                NgrokUrl = match.Groups[1].Value;
                _logger.LogInformation($"Ngrok tunnel to: {NgrokUrl}");
                timer.Start();

                //post api
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("Issuer"));
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ObsToken);
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/movie/video-api", new { domain = NgrokUrl });
                    if (response.IsSuccessStatusCode)
                        _logger.LogInformation($"Update to website success");
                    else
                        _logger.LogError($"Failed to update. {response.StatusCode}");
                }
            }
        }

        public void Dispose()
        {
            timer.Dispose();
            process?.Kill();
            process?.Dispose();
        }

        private class JWT
        {
            [JsonPropertyName("token")]
            public string? Token { get; set; }
        }

        public async Task<bool> Access(string email, string password)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("Issuer"));
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/auth/login", new { email = email, password = password });
                if (response.IsSuccessStatusCode)
                {
                    JWT? token = await response.Content.ReadFromJsonAsync<JWT>();
                    if (token != null && !string.IsNullOrEmpty(token.Token))
                    {
                        ObsToken = token.Token;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
