using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.UI.Domain.Interfaces {
    public interface ITravelImageProvider
    {
        Task<Country[]> GetCountries();
    }
}