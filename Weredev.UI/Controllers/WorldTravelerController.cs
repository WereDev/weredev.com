using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Models.WorldTraveler;

namespace Weredev.UI.Controllers
{
    [Route("api/[controller]")]
    public class WorldTravelerController : Controller
    {
        private readonly ITravelImageProvider _travelImageProvider;

        public WorldTravelerController(ITravelImageProvider travelImageProvider) {
            _travelImageProvider = travelImageProvider ?? throw new ArgumentNullException(nameof(travelImageProvider));
        }

        [HttpGet("country")]
        public async Task<ListCountriesResponse> Country() {
            var imageCountries = await _travelImageProvider.GetCountries();
            var response = new ListCountriesResponse();
            response.Countries = imageCountries.Select(x => new Country() {
                Key = x.Key,
                Name = x.Name
            }).ToArray();
            return response;
        }
    }
}