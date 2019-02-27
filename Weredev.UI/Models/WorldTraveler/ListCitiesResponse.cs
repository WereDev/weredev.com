using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler {
    public class ListCitiesResponse {

        public ListCitiesResponse(Country country) {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (!country.Cities.Any()) throw new ArgumentNullException(nameof(country.Cities));

            Country = new CountryModel(country);

            Cities = country.Cities.Select(x => new CityModel(x)).ToArray();
        }

        public CountryModel Country { get; }
        public CityModel[] Cities { get; }
    }
}