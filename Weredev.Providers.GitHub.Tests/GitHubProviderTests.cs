using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Weredev.Providers.GitHub.Tests
{
    public class GitHubProviderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Explicit]
        public async Task GetReadmeMarkdown_WhenMissing_ReturnsNull()
        {
            var provider = new GitHubProvider();
            var content = await provider.GetReadmeMarkdown(Guid.NewGuid().ToString());
            Assert.IsNull(content);
        }

        [Test]
        [Explicit]
        public async Task GetReadmeMarkdown_WhenAvailable_ReturnsDecodedString()
        {
            var provider = new GitHubProvider();
            var content = await provider.GetReadmeMarkdown("Wu10Man");
            Assert.NotNull(content);
            Assert.IsTrue(content.Contains('*'));
        }
    }
}
