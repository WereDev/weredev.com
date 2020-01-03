using System;

namespace Weredev.UI.Domain.Models.Developer
{
    public class ReleaseNotesDomainModel
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public DateTime PublishedAt { get; set; }

        public string Body { get; set; }

        public Asset[] Assets { get; set; }

        public class Asset
        {
            public string Name { get; set; }

            public string DownloadUrl { get; set; }

            public int DownloadCount { get; set; }
        }
    }
}
