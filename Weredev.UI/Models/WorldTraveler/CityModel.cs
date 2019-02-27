using System;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class CityModel
    {
        public CityModel(City city) {
            if (city == null) throw new ArgumentNullException(nameof(city));
            IconUrl = city.IconUrl;
            Id = city.Id;
            Description = city.Description;
            Name = city.Name;
        }
        
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}