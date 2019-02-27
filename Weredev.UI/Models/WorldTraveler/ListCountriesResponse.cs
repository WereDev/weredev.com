using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler {
    public class ListCountriesResponse {

        public ListCountriesResponse(IEnumerable<Country> countries) {
            if (countries == null) throw new ArgumentNullException(nameof(countries));
            
            Countries = countries.Select(x => new CountryModel(x)).ToArray();
        }

        public CountryModel[] Countries { get; set; }
    }
}