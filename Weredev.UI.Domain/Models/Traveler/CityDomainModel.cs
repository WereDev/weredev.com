namespace Weredev.UI.Domain.Models.Traveler
{
    public class CityDomainModel
    {
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
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