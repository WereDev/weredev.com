namespace Weredev.UI.Domain.Models.Traveler
{
    public class CountryDomainModel
    {
        public City[] Cities { get; }

        public string Key { get; set; }

        public string Name { get; set; }

        public class City
        {
            public string IconUrl { get; set; }

            public string Description { get; set; }

            public string Name { get; set; }

            public string Key { get; set; }
        }
    }
}
