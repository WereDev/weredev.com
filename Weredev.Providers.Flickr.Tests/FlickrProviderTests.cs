using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Weredev.Providers.Flickr.Tests
{
    [TestFixture]
    public class FlickrProviderTests
    {
        [Test]
        [Explicit]
        public async Task GetNavigationList_Success()
        {
            using var provider = GetFlickrProvider();
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

        [Test]
        [Explicit]
        public async Task GetPhotosetPhotos()
        {
            using var provider = GetFlickrProvider();
            var photos = await provider.ListPhotos("72157675450153882");
            Assert.IsNotNull(photos?.Photos);
            foreach (var photo in photos.Photos)
            {
                Assert.IsNotNull(photo.Scales);
                Assert.Greater(photo.Scales.Length, 0);
            }
        }

        [Test]
        [Explicit]
        public async Task GetPhotoInfo()
        {
            using var provider = GetFlickrProvider();
            var photo = await provider.GetPhotoInfo("24293698110", "e0099b3948");
            Assert.IsNotNull(photo?.Tags);
        }

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
