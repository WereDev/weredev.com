using System;
using System.Linq;
using System.Threading.Tasks;
using Weredev.Domain.Interfaces;
using Weredev.Domain.Mappers;
using Weredev.Domain.Models.Traveler;
using Weredev.Domain.Models.TravelImageProvider;

namespace Weredev.Domain.Services
{
    public class TravelService : ITravelService
    {
        private const string CountryDomainsCacheKey = "Countries List";
        private const string CollectListCacheKey = "Collection List";
        private const string PhotosetListCacheKey = "Photoset List";
        private const string PhotosetDetailsCacheKey = "Photoset Detail ";

        private readonly ITravelImageProvider _travelImageProvider;
        private readonly ICacheProvider _cacheProvider;

        public TravelService(ITravelImageProvider travelImageProvider, ICacheProvider cacheProvider)
        {
            _travelImageProvider = travelImageProvider ?? throw new ArgumentNullException(nameof(travelImageProvider));
            _cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(travelImageProvider));
        }

        public async Task<CountryDomainModel[]> ListCountries()
        {
            var countries = await ListCountryDomains();
            return countries;
        }

        public async Task<CountryDomainModel> GetCountry(string countryKey)
        {
            var countries = await ListCountries();
            var country = countries.FirstOrDefault(x => x.Key.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase));
            return country;
        }

        public async Task<CityDomainModel> GetCity(string countryKey, string cityKey)
        {
            var collections = await ListCollections();
            var collection = collections.FirstOrDefault(x => x.CountryKey.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase)
                                                        && x.CityKey.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));
            if (collection == null)
                return null;

            var city = collection.ToCityDomainModel();

            var photosets = await ListPhotosets();
            foreach (var album in city.Albums)
            {
                var photoset = photosets.FirstOrDefault(x => x.Id == album.Id);
                album.IconUrl = photoset?.IconUrl;
            }

            return city;
        }

        public async Task<AlbumDomainModel> GetAlbum(string countryKey, string cityKey, string albumKey)
        {
            var collections = await ListCollections();
            var collection = collections.FirstOrDefault(x => x.CountryKey.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase)
                                                        && x.CityKey.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));

            var album = collection?.Albums?.FirstOrDefault(x => x.Key.Equals(albumKey, StringComparison.CurrentCultureIgnoreCase));
            if (album == null)
                return null;

            var photoset = await GetPhotosetDetails(album.Id);

            var returnModel = photoset.ToAlbumDomainModel(collection);
            returnModel.AlbumKey = album.Name;
            returnModel.AlbumName = album.Name;
            return returnModel;
        }

        private async Task<CountryDomainModel[]> ListCountryDomains()
        {
            var countryDomains = _cacheProvider.Get<CountryDomainModel[]>(CountryDomainsCacheKey);
            if (countryDomains == null)
            {
                var navList = await ListCollections();
                countryDomains = navList.ToCountryDomainModels();
                _cacheProvider.Set(CountryDomainsCacheKey, countryDomains);
            }

            return countryDomains;
        }

        private async Task<CollectionProviderModel[]> ListCollections()
        {
            var navList = _cacheProvider.Get<CollectionProviderModel[]>(CollectListCacheKey);
            if (navList == null)
            {
                navList = await _travelImageProvider.ListCollections();
                _cacheProvider.Set(CollectListCacheKey, navList);
            }

            return navList;
        }

        private async Task<PhotosetProviderModel[]> ListPhotosets()
        {
            var photosets = _cacheProvider.Get<PhotosetProviderModel[]>(PhotosetListCacheKey);
            if (photosets == null)
            {
                photosets = await _travelImageProvider.ListPhotosets();
                _cacheProvider.Set(PhotosetListCacheKey, photosets);
            }

            return photosets;
        }

        private async Task<PhotoListProviderModel> GetPhotosetDetails(string photosetId)
        {
            photosetId = photosetId.ToLower().Trim();
            var cacheKey = PhotosetDetailsCacheKey + photosetId;
            var photoset = _cacheProvider.Get<PhotoListProviderModel>(cacheKey);
            if (photoset == null)
            {
                photoset = await _travelImageProvider.ListPhotos(photosetId);
                Parallel.ForEach(photoset.Photos, (photo) => SetPhotoDetails(ref photo));
                _cacheProvider.Set(cacheKey, photoset);
            }

            return photoset;
        }

        private void SetPhotoDetails(ref PhotoListProviderModel.Photo photo)
        {
            var details = _travelImageProvider.GetPhotoInfo(photo.Id, photo.Secret).Result;
            photo.DateTaken = details.DateTaken;
            photo.Rotation = details.Rotation;
            photo.Tags = details.Tags;
            photo.Title = details.Title;
        }
    }
}
