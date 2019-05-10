using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Domain.Interfaces {
    public interface ITravelService {
        Task<Country[]> ListCountries();
        Task<Country> GetCountry(string countryKey);
    }
}