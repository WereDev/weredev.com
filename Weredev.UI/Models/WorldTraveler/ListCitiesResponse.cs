using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListCitiesResponse
    {
        public ListCitiesResponse(CountryDomainModel country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            if (!country.Cities.Any())
                throw new ArgumentNullException(nameof(country.Cities));

            CountryKey = country.Key;
            CountryName = country.Name;
            Cities = country.Cities.Select(x => new CityViewModel(country, x)).ToArray();
        }

        public string CountryKey { get; set; }

        public string CountryName { get; set; }

        public CityViewModel[] Cities { get; }

        public class CityViewModel
        {
            public CityViewModel(CountryDomainModel country, CountryDomainModel.City city)
            {
                if (city == null)
                    throw new ArgumentNullException(nameof(city));
                IconUrl = city.IconUrl;
                Description = city.Description;
                CityName = city.Name;
                CityKey = city.Key;
                CountryKey = country.Key;
                CountryName = country.Name;
            }

            public string CountryKey { get; set; }

            public string CountryName { get; set; }

            public string CityKey { get; set; }

            public string CityName { get; set; }

            public string IconUrl { get; }

            public string Description { get; }
        }
    }
}
