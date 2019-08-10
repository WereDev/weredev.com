using System;

namespace Weredev.UI.Domain.Models.TravelImageProvider
{
    public class PhotoListProviderModel
    {
        public string Id { get; set; }

        public string Primary { get; set; }

        public int Page { get; set; }

        public int PerPage { get; set; }

        public int Pages { get; set; }

        public int Total { get; set; }

        public Photo[] Photos { get; set; }

        public class Photo
        {
            public string Name { get; set; }

            public string[] Tags { get; set; }

            public PhotoScale[] Scales { get; set; }

            public string Secret { get; set; }

            public DateTime? DateTaken { get; set; }

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