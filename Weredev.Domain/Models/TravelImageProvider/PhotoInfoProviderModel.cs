using System;

namespace Weredev.Domain.Models.TravelImageProvider
{
    public class PhotoInfoProviderModel
    {
        public string Id { get; set; }

        public int Rotation { get; set; }

        public string Title { get; set; }

        public DateTime DateTaken { get; set; }

        public string[] Tags { get; set; }

        public string PhotoPageUrl { get; set; }
    }
}
