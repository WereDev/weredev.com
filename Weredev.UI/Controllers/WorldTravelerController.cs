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
        private readonly ITravelService _travelService;

        public WorldTravelerController(ITravelService travelService) {
            _travelService = travelService ?? throw new ArgumentNullException(nameof(travelService));
        }

        [HttpGet]
        public async Task<ActionResult> Index() {
            var countries = await _travelService.ListCountries();
            if (countries == null)
                return NotFound();

            ViewData["Title"] = "world traveler";

            var countriesResponse = new ListCountriesResponse(countries);
            return View(countriesResponse);
        }

        [HttpGet("[controller]/{countryKey}")]
        public async Task<ActionResult<ListCitiesResponse>> ListCities(string countryKey)
        {
            var country = await _travelService.GetCountry(countryKey);
            
            if (country == null) return NotFound();

            ViewData["Title"] = $"world traveler | {country.Name.ToLower()}";

            var citiesResponse = new ListCitiesResponse(country);
            return View(citiesResponse);
        }

        [HttpGet("[controller]/{countryKey}/{cityKey}")]
        public async Task<ActionResult> CityDetails(string countryKey, string cityKey)
        {
            // var countries = await _travelImageProvider.ListCountries();
            // var country = countries?.FirstOrDefault(x => x.Key.Equals(countryKey, StringComparison.CurrentCultureIgnoreCase));
            // if (country == null) return NotFound();

            // var city = country.Cities.FirstOrDefault(x => x.Key.Equals(cityKey, StringComparison.CurrentCultureIgnoreCase));
            // if (city == null) return NotFound();
            

            // return Content(city.Id);
            return Content("Not done yet");
        }
    }
}