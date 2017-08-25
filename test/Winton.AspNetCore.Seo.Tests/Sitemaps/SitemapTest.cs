using System.Runtime.Serialization;
using Castle.Core.Internal;
using FluentAssertions;
using Winton.AspNetCore.Seo.Sitemaps;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Sitemaps
{
    public class SitemapTest
    {
        [Fact]
        private void ShouldHaveCollectionDataContractAttribute()
        {
            typeof(Sitemap).Should().BeDecoratedWith<CollectionDataContractAttribute>();
        }

        [Fact]
        private void ShouldHaveCorrectDataContractName()
        {
            typeof(Sitemap).GetAttribute<CollectionDataContractAttribute>().Name.Should().BeEquivalentTo("urlset");
        }

        [Fact]
        private void ShouldHaveCorrectDataContractNamespace()
        {
            typeof(Sitemap).GetAttribute<CollectionDataContractAttribute>()
                           .Namespace.Should()
                           .BeEquivalentTo("http://www.sitemaps.org/schemas/sitemap/0.9");
        }
    }
}