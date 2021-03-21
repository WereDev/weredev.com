using System.Text.Json.Serialization;

namespace Weredev.Providers.Flickr.Models
{
    public class PhotosetsGetInfoResponse
    {
        public PhotoInfoModel Photo { get; set; }

        public class PhotoInfoModel
        {
            public string Id { get; set; }

            public int Rotation { get; set; }

            public OwnerInfo Owner { get; set; }

            public ContentModel Title { get; set; }

            public ContentModel Description { get; set; }

            public DatesModel Dates { get; set; }

            public TagsModel Tags { get; set; }

            public UrlContent Urls { get; set; }

            public class ContentModel
            {
                [JsonPropertyName("_content")]
                public string Content { get; set; }

                [JsonPropertyName("type")]
                public string ContentType { get; set; }
            }

            public class DatesModel
            {
                public string Taken { get; set; }
            }

            public class TagsModel
            {
                [JsonPropertyName("tag")]
                public TagInfoModel[] Tags { get; set; }
                public class TagInfoModel
                {
                    public string Raw { get; set; }
                }
            }

            public class OwnerInfo
            {
                public string NsId { get; set; }
            }

            public class UrlContent
            {
                public ContentModel[] Url { get; set; }
            }
        }
    }
}
