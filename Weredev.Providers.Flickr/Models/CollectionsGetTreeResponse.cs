namespace Weredev.Providers.Flickr.Models
{
    public class CollectionsGetTreeResponse
    {
        public string Stat { get; set; }

        public TreeCollection Collections { get; set; }

        public class TreeCollection
        {
            public CollectionItem[] Collection { get; set; }
        }

        public class CollectionItem
        {
            public string Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string IconLarge { get; set; }

            public string IconSmall { get; set; }

            public Set[] Set { get; set; }
        }

        public class Set
        {
            public string Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
        }
    }
}
