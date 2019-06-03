namespace Weredev.Providers.Flickr.Models
{
    public class PhotoSetsGetListResponse
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
        }

        public class Content
        {
            public string _Content { get; set; }
        }
    }
}