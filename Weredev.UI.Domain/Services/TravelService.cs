using System;
using System.Linq;
using System.Threading.Tasks;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Domain.Services {
    public class TravelService : ITravelService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ITravelImageProvider _travelImageProvider;
        private readonly string _cacheKey_Countries = "countries";

        public TravelService(ICacheProvider cacheProvider, ITravelImageProvider travelImageProvider)
        {
            _cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
            _travelImageProvider = travelImageProvider ?? throw new ArgumentNullException(nameof(travelImageProvider));
        }

        public async Task<Country[]> ListCountries()
        {
            var countries = _cacheProvider.Get<Country[]>(_cacheKey_Countries);
            if (countries == null) {
                countries = await _travelImageProvider.ListCountries();
                _cacheProvider.Set(_cacheKey_Countries, countries);
            }
            return countries;
        }

        public async Task<Country> GetCountry(string countryKey) {
            var countries = await ListCountries();
            var country = countries?.FirstOrDefault(x => x.Key.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase));
            return country;
        }
    }
}