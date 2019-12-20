using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weredev.UI.Domain.Interfaces;

namespace Weredev.Providers.GitHub
{
    public class GitHubProvider : IGitHubProvider, IDisposable
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private HttpClient _httpClient;

        public GitHubProvider()
        {
            _httpClient = CreateHttpClient();

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public async Task<string> GetReadmeMarkdown(string repoKey)
        {
            var url = $"repos/weredev/{repoKey}/contents/README.md";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _httpClient.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return null;
                case HttpStatusCode.OK:
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(responseContent))
                        return null;
                    var deserialized = JsonSerializer.Deserialize<Models.GetReadmeMarkdownResponse>(responseContent, _jsonSerializerOptions);
                    var encodedMarkup = deserialized.Content.Replace('\n', '\n');
                    var bytes = Convert.FromBase64String(encodedMarkup);
                    var decodedMarkup = Encoding.UTF8.GetString(bytes);
                    return decodedMarkup;
                default:
                    throw new HttpRequestException($"Error getting Readme.md from GitHub {repoKey}: {response.StatusCode}");
            }
        }

        public Task GetReleaseInfo(string repoKey)
        {
            throw new System.NotImplementedException();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.github.com/"),
            };

            httpClient.DefaultRequestHeaders.Add("User-Agent", ".Net Core 3.1.100");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

            return httpClient;
        }
    }
}
