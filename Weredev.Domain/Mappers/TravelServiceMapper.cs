using System;
using System.Collections.Generic;
using System.Linq;
using Weredev.Domain.Models.Traveler;
using Weredev.Domain.Models.TravelImageProvider;

namespace Weredev.Domain.Mappers
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
                Albums = item.Albums.Select(x => x.ToAlbum()).ToList(),
                Description = item.Description,
                CityKey = item.CityKey,
                CityName = item.CityName,
                CountryKey = item.CountryKey,
                CountryName = item.CountryName,
            };
        }

        public static AlbumDomainModel ToAlbumDomainModel(this PhotoListProviderModel model, CollectionProviderModel city)
        {
            return new AlbumDomainModel
            {
                CityKey = city.CityKey,
                CityName = city.CityName,
                CountryKey = city.CountryKey,
                CountryName = city.CountryName,
                Photos = model.Photos.Select(x => x.ToPhoto()).ToArray(),
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

        private static AlbumDomainModel.Photo ToPhoto(this PhotoListProviderModel.Photo photo)
        {
            return new AlbumDomainModel.Photo
            {
                DateTaken = photo.DateTaken,
                Name = photo.Name,
                Scales = photo.Scales.Select(x => new AlbumDomainModel.Photo.PhotoScale
                    {
                        Height = x.Height,
                        Scale = Enum.Parse<AlbumDomainModel.Photo.PhotoScale.ScaleType>(x.Scale.ToString()),
                        Url = x.Url,
                        Width = x.Width,
                    }).ToArray(),
                Secret = photo.Secret,
                Tags = photo.Tags,
                Rotation = photo.Rotation,
            };
        }
    }
}
