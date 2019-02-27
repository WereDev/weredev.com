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

            ViewData["Title"] = "world traveler";

            var countriesResponse = new ListCountriesResponse(countries);
            return View(countriesResponse);
        }

        [HttpGet("[controller]/{countryId}")]
        public async Task<ActionResult<ListCitiesResponse>> ListCities(string countryId)
        {
            var countries = await _travelImageProvider.GetCountries();
            var country = countries?.FirstOrDefault(x => x.Key.Equals(countryId, StringComparison.CurrentCultureIgnoreCase));
            
            if (country == null) return NotFound();

            ViewData["Title"] = $"world traveler | {country.Name.ToLower()}";

            var citiesResponse = new ListCitiesResponse(country);
            return View(citiesResponse);
        }
    }
}