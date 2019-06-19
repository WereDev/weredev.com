
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weredev.Providers.Flickr.Mappers;
using Weredev.UI.Domain.Extensions;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.Traveler;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr
{

    public class FlickrProvider : IDisposable, ITravelImageProvider
    {

        private const string _RequestBaseUrl = "https://api.flickr.com/services/rest/?";
        private const string _Format = "json";
        private const string _NoJsonCallback = "1";
        private const string _FlickrGetTree = "flickr.collections.getTree";
        private const string _FlickrGetInfo= "flickr.collections.getInfo";
        private const string _FlickrGetList = "flickr.photosets.getList";
        private const string _FlickrGetPhotos = "flickr.photosets.getPhotos";
        private readonly string _ApiKey;
        private readonly string _UserId;
        private HttpClient _httpClient;

        public FlickrProvider(string apiKey, string userId)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            _ApiKey = apiKey;
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            _UserId = userId;
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public async Task<CollectionProviderModel[]> ListCollections()
        {
            var flickrTree = await ExecuteFlickrRequestAsync<Models.CollectionsGetTreeResponse>(_FlickrGetTree);
            var domainModels = flickrTree.Collections.Collection.Select(x => x.ParseTreeResponse())
                                                                .ToArray();
            return domainModels;
        }

        public async Task<PhotosetProviderModel[]> ListPhotosets()
        {
            var parameters = new Dictionary<string, string>
            {
                { "primary_photo_extras", "url_s" }
            };

            var photoset = await ExecuteFlickrRequestAsync<Models.PhotosetsGetListResponse>(_FlickrGetList, parameters);
            var models = photoset.ToProviderModel();
            return models;
        }

        private async Task<T> ExecuteFlickrRequestAsync<T>(string flickrMethod, Dictionary<string, string> parameters = null)
        {
            var url = CreateFlickrRequestUrl(flickrMethod, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"Could not get response from Flickr Request {flickrMethod}: {response.StatusCode}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (flickrMethod == _FlickrGetPhotos)
                throw new Exception(responseContent);
            var deserialized = JsonConvert.DeserializeObject<T>(responseContent);
            return deserialized;
        }

        private string CreateFlickrRequestUrl(string flickrMethod, Dictionary<string, string> parameters)
        {

            if (string.IsNullOrWhiteSpace(flickrMethod))
                throw new ArgumentNullException(nameof(flickrMethod));

            var queryString = string.Format("{0}&api_key={1}&user_id={2}&format={3}&nojsoncallback={4}&method={5}",
                _RequestBaseUrl,
                _ApiKey,
                _UserId,
                _Format,
                _NoJsonCallback,
                flickrMethod
                );

            if (parameters != null && parameters.Any())
            {
                var querySegments = parameters.Select(x => $"{x.Key}={x.Value}");
                var querySuffix = string.Join("&", querySegments);
                queryString += "&" + querySuffix;
            }

            return queryString;
        }
    }
}