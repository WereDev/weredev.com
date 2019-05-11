using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Weredev.Providers.Flickr.Tests
{
    [TestClass]
    public class FlickrProviderTests
    {
        [TestMethod]
        public async Task GetCountries_Success()
        {
            var provider = GetFlickrProvider();
            var countries = await provider.ListCountries();
            Assert.IsNotNull(countries);
            Assert.AreNotEqual(0, countries.Length);
            foreach (var country in countries) {
                Assert.IsFalse(string.IsNullOrWhiteSpace(country.Key));
                Assert.IsFalse(string.IsNullOrWhiteSpace(country.Name));
                Assert.IsNotNull(country.Cities);
                Assert.AreNotEqual(0, country.Cities.Count);
            }
        }

        private FlickrProvider GetFlickrProvider() {
                var config = new ConfigurationBuilder()
                            .AddJsonFile("weredev.com.config")
                            .Build();
                var apiKey = config.GetValue<string>("Flickr.ApiKey");
                var userId = config.GetValue<string>("Flickr.UserId");
                return new FlickrProvider(apiKey, userId);
        }
    }
}
