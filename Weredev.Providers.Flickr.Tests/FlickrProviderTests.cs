using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Weredev.UI.Domain.Interfaces;

namespace Weredev.Providers.Flickr.Tests
{
    [TestFixture]
    public class FlickrProviderTests
    {
        [Test]
        [Explicit]
        public async Task GetNavigationList_Success()
        {
            using (var provider = GetFlickrProvider())
            {
                var navList = await provider.ListCollections();
                Assert.IsNotNull(navList);
                Assert.Greater(navList.Length, 0);
                foreach (var navItem in navList)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Icon));
                    Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Id));
                    Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Title));
                }
            }
        }

        [Test]
        [Explicit]
        public async Task GetPhotosetPhotos()
        {
            using (var provider = GetFlickrProvider())
            {
                var photos = await provider.ListPhotos("72157675450153882");
                Assert.IsNotNull(photos?.Photos);
                foreach (var photo in photos.Photos)
                {
                    Assert.IsNotNull(photo.Scales);
                    Assert.Greater(photo.Scales.Length, 0);
                }
            }
        }

        // [Test]
        // public async Task GetCountryInfo() {
        //     var provider = GetFlickrProvider();
        //     var countries = await provider.ListCountries();
        //     var countryKey = countries[0].Key;
        //     var cityKey = countries[0].Cities[0].Key;
        //     await provider.GetCityInfo(countryKey, cityKey);
        // }

        private FlickrProvider GetFlickrProvider()
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("weredev.com.config")
                        .Build();
            var apiKey = config.GetValue<string>("Flickr.ApiKey");
            var userId = config.GetValue<string>("Flickr.UserId");
            return new FlickrProvider(apiKey, userId);
        }
    }
}
