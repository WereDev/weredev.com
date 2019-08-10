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
        private const string CountryDomainsCacheKey = "Country Dictionary Cache Key";
        private const string CollectListCacheKey = "Collection List";
        private const string PhotosetListCacheKey = "Photoset List";

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
            countryKey = countryKey?.ToLower();
            var country = countries.FirstOrDefault(x => x.Key.ToLower() == countryKey);
            return country;
        }

        public async Task<CityDomainModel> GetCity(string countryKey, string cityKey)
        {
            var collections = await ListCollections();
            var collection = collections.FirstOrDefault(x => x.CountryKey.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase)
                                                        && x.CityKey.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));
            if (collection == null)
                return null;

            var photosets = await ListPhotosets();

            var city = collection.ToCityDomainModel();

            foreach (var album in city.Albums)
            {
                var photoset = photosets.FirstOrDefault(x => x.Id == album.Id);
                album.IconUrl = photoset?.IconUrl;
            }

            return city;
        }

        public Task<AlbumDomainModel> GetAlbum(string countryKey, string cityKey, string albumKey)
        {
            throw new NotImplementedException();
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
    }
}
