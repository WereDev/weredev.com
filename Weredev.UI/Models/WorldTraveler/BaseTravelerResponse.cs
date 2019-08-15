namespace Weredev.UI.Models.WorldTraveler
{
    public abstract class BaseTravelerResponse
    {
        public string CountryKey { get; set; }

        public string CountryName { get; set; }

        public string CityKey { get; set; }

        public string CityName { get; set; }

        public string AlbumKey { get; set; }

        public string AlbumName { get; set; }

        public string BaseUrl => "/worldtraveler";
        
        public string CountryUrl => $"{BaseUrl}/{CountryKey}";

        public string CityUrl => $"{CountryUrl}/{CityKey}";

        public string AlbumUrl => $"{CityUrl}/{AlbumKey}";
    }
}
