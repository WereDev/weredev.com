using System;
using System.Linq;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Models.WorldTraveler
{
    public class ListPhotosResponse : BaseTravelerResponse
    {
        public ListPhotosResponse(AlbumDomainModel album)
        {
            AlbumKey = album.AlbumKey;
            AlbumName = album.AlbumName;
            CityKey = album.CityKey;
            CityName = album.CityName;
            CountryKey = album.CountryKey;
            CountryName = album.CountryName;
            Photos = album.Photos.Select(x => new Photo(x)).ToArray();
        }

        public Photo[] Photos { get; }

        public class Photo
        {
            public Photo(AlbumDomainModel.Photo photo)
            {
                DateTaken = photo.DateTaken;
                Name = photo.Name;
                Scales = photo.Scales.Select(x => new PhotoScale(x)).ToArray();
                Secret = photo.Secret;
                Tags = photo.Tags;
            }

            public string Name { get; set; }

            public string[] Tags { get; set; }

            public PhotoScale[] Scales { get; set; }

            public string Secret { get; set; }

            public DateTime? DateTaken { get; set; }

            public class PhotoScale
            {
                public PhotoScale(AlbumDomainModel.Photo.PhotoScale scale)
                {
                    Height = scale.Height;
                    Scale = Enum.Parse<ScaleType>(scale.Scale.ToString());
                    Url = scale.Url;
                    Width = scale.Width;
                }

                public enum ScaleType
                {
                    Original,
                    Thumbnail,
                    Small,
                    Medium,
                }

                public string Url { get; set; }

                public int Height { get; set; }

                public int Width { get; set; }

                public ScaleType Scale { get; set; }
            }
        }
    }
}
