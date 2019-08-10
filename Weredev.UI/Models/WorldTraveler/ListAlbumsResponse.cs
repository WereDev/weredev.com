using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

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
            Albums = city.Albums.Select(x => new Album(x)).ToArray();
        }

        public string CountryKey { get; }

        public string CountryName { get; }

        public string CityKey { get; }

        public string CityName { get; }

        public Album[] Albums { get; }

        public class Album
        {
            public Album(CityDomainModel.Album album)
            {
                if (album == null)
                    throw new ArgumentNullException(nameof(album));

                Name = album.Name;
                Description = album.Description;
                Key = album.Key;
                IconUrl = album.IconUrl;
            }

            public string Name { get; }

            public string Description { get; }

            public string Key { get; }

            public string IconUrl { get; }
        }
    }
}
