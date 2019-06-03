using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.UI.Domain.Extensions;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.Traveler;
using Weredev.UI.Domain.Models.TravelImageProvider;

namespace Weredev.UI.Domain.Mappers
{
    public static class TravelServiceMapper
    {
        public static CountryDomainModel[] ToCountryDomainModels(this CollectionDomainModel[] navModels)
        {
            var countryDictionary = new Dictionary<string, CountryDomainModel>();

            foreach (var navModel in navModels)
            {
                ParseNavigationModel(navModel, countryDictionary);
            }

            return countryDictionary.Values.ToArray();
        }

        private static void ParseNavigationModel(CollectionDomainModel navModel, Dictionary<string, CountryDomainModel> countryDictionary)
        {
            if (!countryDictionary.ContainsKey(navModel.CountryKey))
            {
                countryDictionary.Add(navModel.CountryKey,
                                      new CountryDomainModel
                                      {
                                          Key = navModel.CountryKey,
                                          Name = navModel.CountryName
                                      });
            }

            var city = new CountryDomainModel.City()
            {
                Description = navModel.Description,
                IconUrl = navModel.Icon,
                Name = navModel.CityName,
                Key = navModel.CityKey
            };

            countryDictionary[navModel.CountryKey].Cities.Add(city);
        }

        public static CityDomainModel ToCityDomainModel(this CollectionDomainModel item)
        {
            return new CityDomainModel
            {
                Albums = item.Albums.Select(x => x.ToAlbum()).ToArray(),
                Description = item.Description,
                Name = item.CityName,
                Key = item.CityKey
            };
        }

        private static CityDomainModel.Album ToAlbum(this CollectionDomainModel.Album album)
        {
            return new CityDomainModel.Album
            {
                Description = album.Description,
                Key = album.Key,
                Name = album.Name
            };
        }
    }
}