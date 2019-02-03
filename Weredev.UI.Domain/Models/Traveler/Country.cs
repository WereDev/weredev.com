using System;
using System.Collections.Generic;

namespace Weredev.UI.Domain.Models.Traveler
{
    public class Country
    {
        public Country()
        {
            Cities = new List<City>();
        }
        
        public List<City> Cities { get; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}