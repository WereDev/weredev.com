using Newtonsoft.Json;

namespace Weredev.Providers.Flickr.Models
{
    public class PhotosetsGetPhotosResponse
    {
        [JsonProperty(PropertyName="photoset")]
        public PhotoSet Set { get; set; }

        [JsonProperty(PropertyName="stat")]
        public string Status { get; set; }

        public class PhotoSet
        {
            [JsonProperty(PropertyName="id")]
            public string Id { get; set; }
            
            [JsonProperty(PropertyName="primary")]
            public string PrimaryName { get; set; }

            [JsonProperty(PropertyName="page")]
            public string Page { get; set; }
            
            [JsonProperty(PropertyName="perpage")]
            public string PerPage { get; set; }

            [JsonProperty(PropertyName="pages")]
            public string TotalPages { get; set; }

            [JsonProperty(PropertyName="total")]
            public string TotalPhotos { get; set; }

            [JsonProperty(PropertyName="photo")]
            public Photo[] Photos { get; set; }

            public class Photo
            {
                [JsonProperty(PropertyName="id")]
                public string Id { get; set; }

                [JsonProperty(PropertyName="secret")]
                public string Secret { get; set; }

                [JsonProperty(PropertyName="server")]
                public string Server { get; set; }

                [JsonProperty(PropertyName="farm")]
                public string Farm { get; set; }

                [JsonProperty(PropertyName="title")]
                public string Title { get; set; }

                [JsonProperty(PropertyName="isprimary")]
                public string IsPrimary { get; set; }

                [JsonProperty(PropertyName="ispublic")]
                public string IsPublic { get; set; }

                [JsonProperty(PropertyName="isfriend")]
                public string IsFriend { get; set; }

                [JsonProperty(PropertyName="isfamily")]
                public string IsFamily { get; set; }

                [JsonProperty(PropertyName="datetaken")]
                public string DateTaken { get; set; }

                [JsonProperty(PropertyName="url_t")]
                public string ThumbnailUrl { get; set; }

                [JsonProperty(PropertyName="height_t")]
                public string ThumbnailHeight { get; set; }

                [JsonProperty(PropertyName="width_t")]
                public string ThumbnailWidth { get; set; }

                [JsonProperty(PropertyName="url_s")]
                public string SmallUrl { get; set; }

                [JsonProperty(PropertyName="height_s")]
                public string SmallHeight { get; set; }

                [JsonProperty(PropertyName="width_s")]
                public string SmallWidth { get; set; }

                [JsonProperty(PropertyName="url_m")]
                public string MediumUrl { get; set; }

                [JsonProperty(PropertyName="height_m")]
                public string MediumHeight { get; set; }

                [JsonProperty(PropertyName="width_m")]
                public string MediumWidth { get; set; }

                [JsonProperty(PropertyName="url_o")]
                public string OriginalUrl { get; set; }

                [JsonProperty(PropertyName="height_o")]
                public string OriginalHeight { get; set; }

                [JsonProperty(PropertyName="width_o")]
                public string OriginalWidth { get; set; }
            }
        }
    }
}