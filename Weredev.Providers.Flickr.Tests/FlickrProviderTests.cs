using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Weredev.UI.Domain.Interfaces;

namespace Weredev.Providers.Flickr.Tests
{
    [TestClass]
    public class FlickrProviderTests
    {
        [TestMethod]
        public async Task GetNavigationList_Success()
        {
            var provider = GetFlickrProvider();
            var navList = await provider.ListCollections();
            Assert.IsNotNull(navList);
            Assert.AreNotEqual(0, navList.Length);
            foreach (var navItem in navList) {
                Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Icon));
                Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Id));
                Assert.IsFalse(string.IsNullOrWhiteSpace(navItem.Title));
            }
        }

        // [TestMethod]
        // public async Task GetCountryInfo() {
        //     var provider = GetFlickrProvider();
        //     var countries = await provider.ListCountries();
        //     var countryKey = countries[0].Key;
        //     var cityKey = countries[0].Cities[0].Key;
        //     await provider.GetCityInfo(countryKey, cityKey);
        // }

        private FlickrProvider GetFlickrProvider() {
                var config = new ConfigurationBuilder()
                            .AddJsonFile("weredev.com.config")
                            .Build();
                var apiKey = config.GetValue<string>("Flickr.ApiKey");
                var userId = config.GetValue<string>("Flickr.UserId");
                return new FlickrProvider(apiKey, userId);
        }


        private ICacheProvider GenerateMoqCacheProvider() {
            var provider = new Mock<ICacheProvider>();
            return provider.Object;
        }
    }
}
