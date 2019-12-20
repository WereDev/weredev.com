using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Weredev.Providers.Flickr.Mappers;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr
{
    public class FlickrProvider : IDisposable, ITravelImageProvider
    {
        private const string _RequestBaseUrl = "https://api.flickr.com/services/rest/?";
        private const string _Format = "json";
        private const string _NoJsonCallback = "1";
        private const string _FlickrGetTree = "flickr.collections.getTree";
        private const string _FlickrGetList = "flickr.photosets.getList";
        private const string _FlickrGetPhotos = "flickr.photosets.getPhotos";
        private const string _FlickrGetPhotoInfo = "flickr.photos.getInfo";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _apiKey;
        private readonly string _userId;
        private HttpClient _httpClient;

        public FlickrProvider(string apiKey, string userId)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            _apiKey = apiKey;
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));
            _userId = userId;
            _jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
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
                { "primary_photo_extras", "url_s" },
            };

            var photoset = await ExecuteFlickrRequestAsync<Models.PhotosetsGetListResponse>(_FlickrGetList, parameters);
            var models = photoset.ToProviderModel();
            return models;
        }

        public async Task<PhotoListProviderModel> ListPhotos(string photosetId)
        {
            if (string.IsNullOrWhiteSpace(photosetId))
                throw new ArgumentNullException(nameof(photosetId));

            var parameters = new Dictionary<string, string>
            {
                { "photoset_id", photosetId },
                { "extras", "date_taken, o_dims, url_t, url_s, url_m, url_o" },
            };

            var photos = await ExecuteFlickrRequestAsync<Models.PhotosetsGetPhotosResponse>(_FlickrGetPhotos, parameters);
            var model = photos.ToProviderModel();
            return model;
        }

        public async Task<PhotoInfoProviderModel> GetPhotoInfo(string photoId, string secret)
        {
            if (string.IsNullOrWhiteSpace(photoId))
                throw new ArgumentNullException(nameof(photoId));
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentNullException(nameof(secret));

            var parameters = new Dictionary<string, string>
            {
                { "photo_id", photoId },
                { "secret", secret },
            };

            var photoInfo = await ExecuteFlickrRequestAsync<Models.PhotosetsGetInfoResponse>(_FlickrGetPhotoInfo, parameters);
            var model = photoInfo.ToProviderModel();
            return model;
        }

        private async Task<T> ExecuteFlickrRequestAsync<T>(string flickrMethod, Dictionary<string, string> parameters = null)
        {
            var url = CreateFlickrRequestUrl(flickrMethod, parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _httpClient.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"Could not get response from Flickr Request {flickrMethod}: {response.StatusCode}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions);
            return deserialized;
        }

        private string CreateFlickrRequestUrl(string flickrMethod, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(flickrMethod))
                throw new ArgumentNullException(nameof(flickrMethod));

            var queryString = $"{_RequestBaseUrl}&api_key={_apiKey}&user_id={_userId}&format={_Format}&nojsoncallback={_NoJsonCallback}&method={flickrMethod}";

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
