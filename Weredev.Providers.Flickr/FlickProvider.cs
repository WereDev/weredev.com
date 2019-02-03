
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace Weredev.Providers.Flickr {

    public class FlickrProvider {
        
        private readonly string _RequestBaseUrl = "https://api.flickr.com/services/rest/?";
        private readonly string _Format = "json";
        private readonly string _NoJsonCallback = "1";
        private readonly string _ApiKey;
        private readonly string _UserId;
        private readonly HttpClient _httpClient;

        public FlickrProvider(string apiKey, string userId) {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            _ApiKey = apiKey;
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            _UserId = userId;

            _httpClient = new HttpClient();
        }

        public async void GetCollections_Async() {

        }

        private string CreateFlickrRequestUrl(Dictionary<string, string> parameters) {

            var sb = new StringBuilder();
            foreach(var parameter in parameters) {
                sb.AppendFormat("&{0}={1}", parameter.Key, System.Net.WebUtility.UrlEncode(parameter.Value));
            }

            return string.Format("{0}&api_key={1}&user_id={1}&format={2}&nojsoncallback={3}{4}",
                _RequestBaseUrl,
                _ApiKey,
                _UserId,
                _Format,
                _NoJsonCallback,
                sb.ToString()
                );

        }
    }
}