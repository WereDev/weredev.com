using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Weredev.Domain.Interfaces;
using Weredev.Domain.Models.Developer;

namespace Weredev.Providers.GitHub
{
    public class GitHubProvider : IGitHubProvider, IDisposable
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly Mapper _mapper;
        private HttpClient _httpClient;

        public GitHubProvider()
        {
            _httpClient = CreateHttpClient();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GitHubMapperProfile>();
            });
            _mapper = new Mapper(mapperConfig);

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
            var url = $"{repoKey}/contents/README.md";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _httpClient.SendAsync(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return null;
                case HttpStatusCode.OK:
                    var responseModel = await ProcessResponse<Models.GetReadmeMarkdownResponse>(response);
                    var encodedMarkup = responseModel.Content.Replace('\n', '\n');
                    var bytes = Convert.FromBase64String(encodedMarkup);
                    var decodedMarkup = Encoding.UTF8.GetString(bytes);
                    return decodedMarkup;
                default:
                    throw new HttpRequestException($"Error getting Readme.md from GitHub {repoKey}: {response.StatusCode}");
            }
        }

        public async Task<ReleaseNotesDomainModel[]> GetReleaseInfo(string repoKey)
        {
            var url = $"{repoKey}/releases";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _httpClient.SendAsync(request);
            var responseModel = await ProcessResponse<Models.GetReleaseNotesResponse[]>(response);

            var mapped = responseModel.Select(x => _mapper.Map<ReleaseNotesDomainModel>(x)).ToArray();
            return mapped;
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.github.com/repos/weredev/"),
            };

            httpClient.DefaultRequestHeaders.Add("User-Agent", ".Net Core 3.1.100");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

            return httpClient;
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
            where T : class
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent))
                return null;
            var deserialized = JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions);
            return deserialized;
        }
    }
}
