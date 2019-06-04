using System;
using System.Linq;
using System.Threading.Tasks;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Mappers;
using Weredev.UI.Domain.Models.Traveler;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.UI.Domain.Services
{
    public class TravelService : ITravelService
    {
        private readonly ITravelImageProvider _travelImageProvider;
        private readonly ICacheProvider _cacheProvider;

        const string _countryDomainsCacheKey = "Country Dictionary Cache Key";
        const string _collectListCacheKey = "Collection List";
        const string _photosetListCacheKey = "Photoset List";
        const string _albumCachKey = "Album ";

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
            countryKey = countryKey?.ToLower();
            var country = countries.FirstOrDefault(x => x.Key.ToLower() == countryKey);
            return country;
        }

        public async Task<CityDomainModel> GetCity(string countryKey, string cityKey)
        {
            var collections = await ListCollections();
            var collection = collections.FirstOrDefault(x => x.CountryKey.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase)
                                                        && x.CityKey.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));
            if (collection == null) return null;

            var photosets = await ListPhotosets();

            var city = collection.ToCityDomainModel();

            foreach (var album in city.Albums)
            {
                var photoset = photosets.FirstOrDefault(x => x.Id == album.Id);
                album.IconUrl = photoset?.IconUrl;
            }

            return city;
        }

        private async Task<CountryDomainModel[]> ListCountryDomains()
        {
            var countryDomains = _cacheProvider.Get<CountryDomainModel[]>(_countryDomainsCacheKey);
            if (countryDomains == null)
            {
                var navList = await ListCollections();
                countryDomains = navList.ToCountryDomainModels();
                _cacheProvider.Set(_countryDomainsCacheKey, countryDomains);
            }
            return countryDomains;
        }

        private async Task<CollectionProviderModel[]> ListCollections()
        {
            var navList = _cacheProvider.Get<CollectionProviderModel[]>(_collectListCacheKey);
            if (navList == null)
            {
                navList = await _travelImageProvider.ListCollections();
                _cacheProvider.Set(_collectListCacheKey, navList);
            }
            return navList;
        }

        private async Task<PhotosetProviderModel[]> ListPhotosets()
        {
            var photosets = _cacheProvider.Get<PhotosetProviderModel[]>(_photosetListCacheKey);
            if (photosets == null)
            {
                photosets = await _travelImageProvider.ListPhotosets();
                _cacheProvider.Set(_photosetListCacheKey, photosets);
            }
            return photosets;
        }
    }
}