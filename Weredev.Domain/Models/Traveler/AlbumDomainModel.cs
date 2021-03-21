using System;

namespace Weredev.Domain.Models.Traveler
{
    public class AlbumDomainModel
    {
        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string CountryKey { get; set; }

        public string CountryName { get; set; }

        public string CityKey { get; set; }

        public string CityName { get; set; }

        public string AlbumKey { get; set; }

        public string AlbumName { get; set; }

        public Photo[] Photos { get; set; }

        public class Photo
        {
            public string Name { get; set; }

            public string[] Tags { get; set; }

            public PhotoScale[] Scales { get; set; }

            public string Secret { get; set; }

            public DateTime? DateTaken { get; set; }

            public int Rotation { get; set; }

            public string Description { get; set; }

            public string PhotoPageUrl { get; set; }

            public class PhotoScale
            {
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
