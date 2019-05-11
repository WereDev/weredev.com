using System;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class CityModel
    {
        public CityModel(City city)
        {
            if (city == null) throw new ArgumentNullException(nameof(city));
            IconUrl = city.IconUrl;
            Id = city.Id;
            Description = city.Description;
            Name = city.Name;
            Key = city.Key;
        }

        public string IconUrl { get; }
        public string Id { get; }
        public string Description { get; }
        public string Name { get; }
        public string Key { get; }
    }
}