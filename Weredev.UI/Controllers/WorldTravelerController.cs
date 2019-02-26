using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Models.WorldTraveler;

namespace Weredev.UI.Controllers
{
    public class WorldTravelerController : Controller
    {
        private readonly ITravelImageProvider _travelImageProvider;

        public WorldTravelerController(ITravelImageProvider travelImageProvider) {
            _travelImageProvider = travelImageProvider ?? throw new ArgumentNullException(nameof(travelImageProvider));
        }

        [HttpGet]
        public async Task<ActionResult> Index() {
            var countries = await _travelImageProvider.GetCountries();
            if (countries == null)
                return NotFound();

            var countriesResponse = new ListCountriesResponse(countries);
            return View(countriesResponse);
        }

        // [HttpGet("country/{cityid}")]
        // public async Task<ActionResult<ListCitiesResponse>> Cities(string cityId) {

        //     var countries = await _travelImageProvider.GetCountries();
        //     var country = countries?.FirstOrDefault(x => cityId.Equals(x.Key, StringComparison.CurrentCultureIgnoreCase));

        //     if (country == null)
        //         return NotFound();

        //     var response = new ListCitiesResponse(country.Cities);
        //     return response;
            
        // }
    }
}