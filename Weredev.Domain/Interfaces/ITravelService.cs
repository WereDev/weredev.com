using System.Threading.Tasks;
using Weredev.Domain.Models.Traveler;

namespace Weredev.Domain.Interfaces
{
    public interface ITravelService
    {
        Task<CountryDomainModel[]> ListCountries();

        Task<CountryDomainModel> GetCountry(string countryKey);

        Task<CityDomainModel> GetCity(string countryKey, string cityKey);

        Task<AlbumDomainModel> GetAlbum(string countryKey, string cityKey, string albumKey);
    }
}
