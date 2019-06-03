using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListCountriesResponse
    {
        public ListCountriesResponse(IEnumerable<CountryDomainModel> countries)
        {
            if (countries == null) throw new ArgumentNullException(nameof(countries));
            Countries = countries.Select(x => new CountryViewModel(x)).ToArray();
        }

        public CountryViewModel[] Countries { get; set; }

        public class CountryViewModel
        {
            public CountryViewModel(CountryDomainModel country)
            {
                if (country == null) throw new ArgumentNullException(nameof(country));
                Key = country.Key;
                Name = country.Name;
            }

            public string Key { get; }
            public string Name { get; }
        }
    }
}