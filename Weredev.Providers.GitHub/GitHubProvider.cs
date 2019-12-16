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
        private readonly string _baseUrl = @"https://api.github.com/";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        
        private HttpClient _httpClient;

        public GitHubProvider()
        {
            _httpClient = new HttpClient();
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
            var url = $"{_baseUrl}repos/weredev/${repoKey}/contents/README.md";
            using var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"Error getting Readme.md from GitHub {repoKey}: {response.StatusCode}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<Models.GetReadmeMarkdownResponse>(responseContent, _jsonSerializerOptions);

            var encodedMarkup = deserialized.Content.Replace('\n', '\n');
            var bytes = Convert.FromBase64String(encodedMarkup);
            var decodedMarkup = Encoding.UTF8.GetString(bytes);

            return decodedMarkup;
        }

        public Task GetReleaseInfo(string repoKey)
        {
            throw new System.NotImplementedException();
        }
    }
}
