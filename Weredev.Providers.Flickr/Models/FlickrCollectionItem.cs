namespace Weredev.Providers.Flickr.Models {
    public class FlickrCollectionItem {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconLarge { get; set; }
        public string IconSmall { get; set; }
        public FlickrSet[] Set { get; set; }
    }
}