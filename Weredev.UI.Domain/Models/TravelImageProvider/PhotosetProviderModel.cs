using Weredev.UI.Domain.Extensions;

namespace Weredev.UI.Domain.Models.TravelImageProvider
{
    public class PhotosetProviderModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? IconWidth { get; set; }
        public int? IconHeight { get; set; }
    }
}