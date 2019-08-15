using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListAlbumsResponse : BaseTravelerResponse
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

        public Album[] Albums { get; }

        public class Album : BaseTravelerResponse
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
        }
    }
}
