using System.Threading.Tasks;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.UI.Domain.Interfaces {
    public interface ITravelImageProvider
    {
        Task<CollectionDomainModel[]> ListCollections();
        Task<AlbumDomainModel[]> ListAlbums(string collectionId);
    }
}