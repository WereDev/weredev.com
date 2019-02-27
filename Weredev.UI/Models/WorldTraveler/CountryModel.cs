using System;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class CountryModel
    {
        public CountryModel(Country country) {
            if (country == null) throw new ArgumentNullException(nameof(country));
            Key = country.Key;
            Name = country.Name;
        }
        
        public string Key { get; }
        public string Name { get; }
    }
}