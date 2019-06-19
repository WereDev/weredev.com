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
            public string _Content { get; set; }
        }

        public class PrimaryPhotoExtras
        {
            public string url_s { get; set; }
            public int? height_s { get; set; }
            public int? width_s { get; set; }
        }
    }
}