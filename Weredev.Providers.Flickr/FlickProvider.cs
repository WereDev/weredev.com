
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weredev.Providers.Flickr.Mappers;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.Providers.Flickr {

    public class FlickrProvider : IDisposable, ITravelImageProvider {
        
        private const string _RequestBaseUrl = "https://api.flickr.com/services/rest/?";
        private const string _Format = "json";
        private const string _NoJsonCallback = "1";
        private const string _FlickrGetTree = "flickr.collections.getTree";
        private readonly string _ApiKey;
        private readonly string _UserId;
        private HttpClient _httpClient;


        public FlickrProvider(string apiKey, string userId) {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            _ApiKey = apiKey;
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            _UserId = userId;

            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            if (_httpClient != null) {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public async Task<Country[]> ListCountries() {
            var flickrTree = await ListCollectionsAsync();
            var countries = flickrTree.ToCountries();
            return countries;
        }

        private async Task<Models.FlickrGetTreeResponse> ListCollectionsAsync() {
            var response = await ExecuteFlickrRequestAsync(_FlickrGetTree);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException("Could not load Collections from Flickr: " + response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var flickrGetTreeResponse = JsonConvert.DeserializeObject<Models.FlickrGetTreeResponse>(responseContent);
            return flickrGetTreeResponse;
        }

        private async Task<HttpResponseMessage> ExecuteFlickrRequestAsync(string flickrMethod) {

            var url = CreateFlickrRequestUrl(flickrMethod);
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            return response;
        }

        private string CreateFlickrRequestUrl(string flickrMethod) {

            if (string.IsNullOrWhiteSpace(flickrMethod))
                throw new ArgumentNullException(nameof(flickrMethod));

            return string.Format("{0}&api_key={1}&user_id={2}&format={3}&nojsoncallback={4}&method={5}",
                _RequestBaseUrl,
                _ApiKey,
                _UserId,
                _Format,
                _NoJsonCallback,
                flickrMethod
                );
        }
    }
}