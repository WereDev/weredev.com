using System.Text.Json.Serialization;

namespace Weredev.Providers.Flickr.Models
{
    public class PhotosetsGetPhotosResponse
    {
        [JsonPropertyName("photoset")]
        public PhotoSet Set { get; set; }

        [JsonPropertyName("stat")]
        public string Status { get; set; }

        public class PhotoSet
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("primary")]
            public string PrimaryName { get; set; }

            [JsonPropertyName("page")]
            public int Page { get; set; }

            [JsonPropertyName("perpage")]
            public int PerPage { get; set; }

            [JsonPropertyName("pages")]
            public int TotalPages { get; set; }

            [JsonPropertyName("total")]
            public int TotalPhotos { get; set; }

            [JsonPropertyName("photo")]
            public Photo[] Photos { get; set; }

            public class Photo
            {
                [JsonPropertyName("id")]
                public string Id { get; set; }

                [JsonPropertyName("secret")]
                public string Secret { get; set; }

                [JsonPropertyName("title")]
                public string Title { get; set; }

                [JsonPropertyName("datetaken")]
                public string DateTaken { get; set; }

                [JsonPropertyName("url_t")]
                public string ThumbnailUrl { get; set; }

                [JsonPropertyName("height_t")]
                public int ThumbnailHeight { get; set; }

                [JsonPropertyName("width_t")]
                public int ThumbnailWidth { get; set; }

                [JsonPropertyName("url_s")]
                public string SmallUrl { get; set; }

                [JsonPropertyName("height_s")]
                public int SmallHeight { get; set; }

                [JsonPropertyName("width_s")]
                public int SmallWidth { get; set; }

                [JsonPropertyName("url_m")]
                public string MediumUrl { get; set; }

                [JsonPropertyName("height_m")]
                public int MediumHeight { get; set; }

                [JsonPropertyName("width_m")]
                public int MediumWidth { get; set; }

                [JsonPropertyName("url_o")]
                public string OriginalUrl { get; set; }

                [JsonPropertyName("height_o")]
                public int OriginalHeight { get; set; }

                [JsonPropertyName("width_o")]
                public int OriginalWidth { get; set; }
            }
        }
    }
}
