using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListCountriesResponse
    {
        public ListCountriesResponse(IEnumerable<CountryDomainModel> countries)
        {
            if (countries == null)
                throw new ArgumentNullException(nameof(countries));
            Countries = countries.Select(x => new CountryViewModel(x)).ToArray();
        }

        public CountryViewModel[] Countries { get; set; }

        public class CountryViewModel
        {
            public CountryViewModel(CountryDomainModel country)
            {
                if (country == null)
                    throw new ArgumentNullException(nameof(country));
                CountryKey = country.Key;
                CountryName = country.Name;
            }

            public string CountryKey { get; set; }

            public string CountryName { get; set; }
        }
    }
}
