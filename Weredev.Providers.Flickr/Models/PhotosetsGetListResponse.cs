using System.Text.Json.Serialization;

namespace Weredev.Providers.Flickr.Models
{
    public class PhotosetsGetListResponse
    {
        public SetList Photosets { get; set; }

        public class SetList
        {
            public int Page { get; set; }

            public int Pages { get; set; }

            public int PerPage { get; set; }

            public int Total { get; set; }

            public SetListItem[] PhotoSet { get; set; }
        }

        public class SetListItem
        {
            public string Id { get; set; }

            public int Photos { get; set; }

            public Content Title { get; set; }

            public Content Description { get; set; }

            public PrimaryPhotoExtras Primary_Photo_Extras { get; set; }
        }

        public class Content
        {
            public string Text { get; set; }
        }

        public class PrimaryPhotoExtras
        {
            [JsonPropertyName("url_s")]
            public string SmallUrl { get; set; }

            [JsonPropertyName("height_s")]
            public int? SmallHeight { get; set; }

            [JsonPropertyName("width_s")]
            public int? SmallWidth { get; set; }
        }
    }
}
