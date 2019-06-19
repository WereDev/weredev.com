using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListCitiesResponse
    {
        public ListCitiesResponse(CountryDomainModel country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (!country.Cities.Any()) throw new ArgumentNullException(nameof(country.Cities));

            CountryKey = country.Key;
            CountryName = country.Name;

            Cities = country.Cities.Select(x => new CityViewModel(x)).ToArray();
        }

        public string CountryKey { get; }
        public string CountryName { get; }
        public CityViewModel[] Cities { get; }

        public class CityViewModel
        {
            public CityViewModel(CountryDomainModel.City city)
            {
                if (city == null) throw new ArgumentNullException(nameof(city));
                IconUrl = city.IconUrl;
                Description = city.Description;
                Name = city.Name;
                Key = city.Key;
            }

            public string IconUrl { get; }
            public string Description { get; }
            public string Name { get; }
            public string Key { get; }
        }
    }
}