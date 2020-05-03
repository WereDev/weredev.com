using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weredev.Domain.Interfaces;
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
            SetKeywords();
            SetDescription("I love to travel the world and take a few pictures along the way.");

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
            {
                return RedirectToAction(
                    nameof(ListAlbums),
                    new
                    {
                        countryKey = country.Key,
                        cityKey = country.Cities[0].Key,
                    });
            }

            SetTitle($"world traveler | {country.Name.ToLower()}");
            SetKeywords(country.Name);
            SetDescription($"Pictures of my time in {country.Name}.");

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
            {
                return RedirectToAction(
                    nameof(ListPhotos),
                    new
                    {
                        countryKey = city.CountryKey,
                        cityKey = city.CityKey,
                        albumKey = city.Albums[0].Key,
                    });
            }

            SetTitle($"world traveler | {city.CountryName.ToLower()} | {city.CityName.ToLower()}");
            SetKeywords(city.CountryName, city.CityName);
            SetDescription($"Pictures of my time in {city.CityName}, {city.CountryName}. {city.Description}");

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
            SetKeywords(album.AlbumName, album.CityName, album.CountryName);
            SetDescription($"Pictures of {album.AlbumName} in {album.CityName}, {album.CountryName}.  {album.Description}");

            var response = new ListPhotosResponse(album);
            return View(response);
        }

        protected override void SetKeywords(params string[] otherKeywords)
        {
            var keywords = new List<string>();

            if (otherKeywords?.Length > 0)
                keywords.AddRange(otherKeywords);

            keywords.AddRange(new string[]
            {
                "world traveler",
                "travel",
                "photos",
                "photography",
                "Flickr",
            });
            
            base.SetKeywords(keywords.ToArray());
        }
    }
}
