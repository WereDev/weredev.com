using System;
using System.Linq;
using Weredev.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListAlbumsResponse
    {
        public ListAlbumsResponse(CityDomainModel city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));
            if (city.Albums?.Any() != true)
                throw new ArgumentNullException(nameof(city.Albums));

            CityKey = city.CityKey;
            CityName = city.CityName;
            CountryKey = city.CountryKey;
            CountryName = city.CountryName;
            Albums = city.Albums.Select(x => new Album(city, x)).ToArray();
        }

        public string CountryKey { get; set; }

        public string CountryName { get; set; }

        public string CityKey { get; set; }

        public string CityName { get; set; }

        public Album[] Albums { get; }

        public class Album
        {
            public Album(CityDomainModel city, CityDomainModel.Album album)
            {
                if (album == null)
                    throw new ArgumentNullException(nameof(album));

                AlbumKey = album.Key;
                AlbumName = album.Name;
                CityKey = city.CityKey;
                CityName = city.CityName;
                CountryKey = city.CountryKey;
                CountryName = city.CountryName;
                Description = album.Description;
                IconUrl = album.IconUrl;
            }

            public string Description { get; }

            public string IconUrl { get; }

            public string CountryKey { get; set; }

            public string CountryName { get; set; }

            public string CityKey { get; set; }

            public string CityName { get; set; }

            public string AlbumKey { get; set; }

            public string AlbumName { get; set; }
        }
    }
}
