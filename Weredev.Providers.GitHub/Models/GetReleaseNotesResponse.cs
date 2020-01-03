using System;
using System.Text.Json.Serialization;

namespace Weredev.Providers.GitHub.Models
{
    public class GetReleaseNotesResponse
    {
        [JsonPropertyName("html_url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        public string Body { get; set; }

        public Asset[] Assets { get; set; }

        public class Asset
        {
            public string Name { get; set; }

            [JsonPropertyName("browser_download_url")]
            public string DownloadUrl { get; set; }

            [JsonPropertyName("download_count")]
            public int DownloadCount { get; set; }
        }
    }
}
