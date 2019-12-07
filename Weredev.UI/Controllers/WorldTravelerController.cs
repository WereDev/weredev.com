using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Models.WorldTraveler;

namespace Weredev.UI.Controllers
{
    public class WorldTravelerController : BaseController
    {
        private readonly ITravelService _travelService;

        public WorldTravelerController(ITravelService travelService)
        {
            _travelService = travelService ?? throw new ArgumentNullException(nameof(travelService));
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var countries = await _travelService.ListCountries();
            if (countries == null)
                return NotFound();

            SetTitle("world traveler");

            var countriesResponse = new ListCountriesResponse(countries);
            return View(countriesResponse);
        }

        [HttpGet("[controller]/{countryKey}")]
        public async Task<ActionResult<ListCitiesResponse>> ListCities(string countryKey)
        {
            var country = await _travelService.GetCountry(countryKey);

            if (country == null || !country.Cities.Any())
                return NotFound();

            if (country.Cities.Count() == 1)
                return Redirect($"{Request.Path.Value}/{country.Cities[0].Key}");

            SetTitle($"world traveler | {country.Name.ToLower()}");

            var citiesResponse = new ListCitiesResponse(country);
            return View(citiesResponse);
        }

        [HttpGet("[controller]/{countryKey}/{cityKey}")]
        public async Task<ActionResult> ListAlbums(string countryKey, string cityKey)
        {
            var city = await _travelService.GetCity(countryKey, cityKey);

            if (city == null || !city.Albums.Any())
                return NotFound();

            if (city.Albums.Count() == 1)
                return Redirect($"{Request.Path.Value}/{city.Albums[0].Key}");

            SetTitle($"world traveler | {city.CountryName.ToLower()} | {city.CityName.ToLower()}");

            var response = new ListAlbumsResponse(city);

            return View(response);
        }

        [HttpGet("[controller]/{countryKey}/{cityKey}/{albumKey}")]
        public async Task<ActionResult> ListPhotos(string countryKey, string cityKey, string albumKey)
        {
            var album = await _travelService.GetAlbum(countryKey, cityKey, albumKey);
            if (album == null)
                return NotFound();

            SetTitle($"world traveler | {album.CountryName.ToLower()} | {album.CityName.ToLower()} | {album.AlbumName.ToLower()}");

            var response = new ListPhotosResponse(album);
            return View(response);
        }
    }
}
