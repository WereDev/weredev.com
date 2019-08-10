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
        public static CountryDomainModel[] ToCountryDomainModels(this CollectionProviderModel[] navModels)
        {
            var countryDictionary = new Dictionary<string, CountryDomainModel>();

            foreach (var navModel in navModels)
            {
                ParseNavigationModel(navModel, countryDictionary);
            }

            return countryDictionary.Values.ToArray();
        }

        public static CityDomainModel ToCityDomainModel(this CollectionProviderModel item)
        {
            return new CityDomainModel
            {
                Albums = item.Albums.Select(x => x.ToAlbum()).ToArray(),
                Description = item.Description,
                CityKey = item.CityKey,
                CityName = item.CityName,
                CountryKey = item.CountryKey,
                CountryName = item.CountryName,
            };
        }

        private static void ParseNavigationModel(CollectionProviderModel navModel, Dictionary<string, CountryDomainModel> countryDictionary)
        {
            if (!countryDictionary.ContainsKey(navModel.CountryKey))
            {
                countryDictionary.Add(
                    navModel.CountryKey,
                    new CountryDomainModel
                    {
                        Key = navModel.CountryKey,
                        Name = navModel.CountryName,
                    });
            }

            var city = new CountryDomainModel.City
            {
                Description = navModel.Description,
                IconUrl = navModel.Icon,
                Name = navModel.CityName,
                Key = navModel.CityKey,
            };

            countryDictionary[navModel.CountryKey].Cities.Add(city);
        }

        private static CityDomainModel.Album ToAlbum(this CollectionProviderModel.Album album)
        {
            return new CityDomainModel.Album
            {
                Description = album.Description,
                Key = album.Key,
                Name = album.Name,
                Id = album.Id,
            };
        }
    }
}
