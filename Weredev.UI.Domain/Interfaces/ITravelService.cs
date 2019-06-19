using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Domain.Interfaces {
    public interface ITravelService {
        Task<CountryDomainModel[]> ListCountries();
        Task<CountryDomainModel> GetCountry(string countryKey);
        Task<CityDomainModel> GetCity(string countryKey, string cityKey);
    }
}