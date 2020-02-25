using System.Threading.Tasks;
using Weredev.Domain.Models.TravelImageProvider;

namespace Weredev.Domain.Interfaces
{
    public interface ITravelImageProvider
    {
        Task<CollectionProviderModel[]> ListCollections();

        Task<PhotosetProviderModel[]> ListPhotosets();

        Task<PhotoListProviderModel> ListPhotos(string photosetId);

        Task<PhotoInfoProviderModel> GetPhotoInfo(string photoId, string secret);
    }
}
