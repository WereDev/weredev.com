using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler {
    public class ListCitiesResponse {

        public ListCitiesResponse(IEnumerable<City> cities) {
            if (cities == null) throw new ArgumentNullException(nameof(cities));

            Cities = cities.Select(x => new CityModel() {
                Description = x.Description,
                IconUrl = x.IconUrl,
                Id = x.Id,
                Name = x.Name
            }).ToArray();

        }

        public CityModel[] Cities { get; set; }
    }
}