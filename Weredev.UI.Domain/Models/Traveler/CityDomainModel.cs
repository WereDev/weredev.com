namespace Weredev.UI.Domain.Models.Traveler
{
    public class CityDomainModel
    {
        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string CountryKey { get; set; }

        public string CountryName { get; set; }

        public string CityKey { get; set; }

        public string CityName { get; set; }

        public Album[] Albums { get; set; }

        public class Album
        {
            public string Description { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }

            public string Key { get; set; }

            public string IconUrl { get; set; }
        }
    }
}
