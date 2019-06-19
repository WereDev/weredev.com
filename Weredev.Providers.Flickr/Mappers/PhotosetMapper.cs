using System.Linq;
using Weredev.Providers.Flickr.Models;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.Providers.Flickr.Mappers
{
    public static class PhotosetMapper
    {
        public static PhotosetProviderModel[] ToProviderModel(this PhotosetsGetListResponse response)
        {
            return response.Photosets.PhotoSet.Select(x =>
                new PhotosetProviderModel
                {
                    Description = x.Description?._Content,
                    IconHeight = x.Primary_Photo_Extras?.height_s,
                    IconUrl = x.Primary_Photo_Extras?.url_s,
                    IconWidth = x.Primary_Photo_Extras?.width_s,
                    Id = x.Id,
                    Name = x.Title?._Content
                }).ToArray();
        }
    }
}