using System;

namespace Weredev.UI.Models.Developer
{
    public class ReleaseNotesResponse : DeveloperResponseBase
    {
        public Release[] Releases { get; set; }
        
        public class Release
        {
            public string Url { get; set; }

            public string Name { get; set; }

            public string SafeTagName => Name.Replace('.', '-');

            public DateTimeOffset PublishedAt { get; set; }

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
}
