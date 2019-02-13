using System.Collections.Generic;
using System.Linq;
using Weredev.Providers.Flickr.Models;
using Weredev.UI.Domain.Extensions;
using Weredev.UI.Domain.Models.Traveler;

namespace Weredev.Providers.Flickr.Mappers
{
    public static class CollectionToCountry
    {
        public static Country[] ToCountries(this FlickrGetTreeResponse flickrResponse) {

            var countryDictionary = new Dictionary<string, Country>();

            foreach (var collection in flickrResponse.Collections.Collection) {
                ParseCollection(countryDictionary, collection);
            }

            return countryDictionary.Values.ToArray();
        }

        private static void ParseCollection(Dictionary<string, Country> countryDictionary, FlickrCollectionItem collectionItem) {

            var nameParts = collectionItem.Title.Split(',');
            var countryName = nameParts[nameParts.Length - 1].Trim();
            var countryKey = countryName.CreateKey();
            if (!countryDictionary.ContainsKey(countryKey)) {
                countryDictionary.Add(countryKey, new Country() {
                    Key = countryKey,
                    Name = countryName
                });
            }

            var city = new City() {
                Description = collectionItem.Description,
                IconUrl = collectionItem.IconLarge,
                Id = collectionItem.Id,
                Name = GetCityName(nameParts)
            };

            countryDictionary[countryKey].Cities.Add(city);
        }

        private static string GetCityName(string[] nameParts)
        {
            var cityName = nameParts[0].Trim();
            if (nameParts.Length > 2) {
                cityName = string.Join(", ", nameParts.Take(nameParts.Length - 1));
            }
            return cityName;
        }
    }
}