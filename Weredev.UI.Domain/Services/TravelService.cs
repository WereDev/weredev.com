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
        const string _navigationListCacheKey = "Navigation List";
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
            var collection = await ListCollections();
            var item = collection.FirstOrDefault(x => x.CountryKey.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase)
                                                        && x.CityKey.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));
            if (item == null) return null;

            var city = item.ToCityDomainModel();
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

        private async Task<CollectionDomainModel[]> ListCollections()
        {
            var navList = _cacheProvider.Get<CollectionDomainModel[]>(_navigationListCacheKey);
            if (navList == null)
            {
                navList = await _travelImageProvider.ListCollections();
                _cacheProvider.Set(_navigationListCacheKey, navList);
            }
            return navList;
        }

        private async Task<AlbumDomainModel> GetAlbumInfo(string albumId)
        {
            // var cacheKey = _albumCachKey + albumId.ToLower().Trim();
            // var album = _cacheProvider.Get<AlbumDomainModel>(cacheKey);
            // if (album == null)
            // {
            //     album = await _travelImageProvider.GetAlbumDetails(albumId);
            //     _cacheProvider.Set(cacheKey, album);
            // }
            // return album;
            return null;
        }
    }
}