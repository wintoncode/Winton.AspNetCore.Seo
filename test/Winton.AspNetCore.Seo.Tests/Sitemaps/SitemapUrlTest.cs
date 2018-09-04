using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Castle.Core.Internal;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    public class SitemapUrlTest
    {
        [Fact]
        private void ShouldHaveCorrectDataContractName()
        {
            typeof(SitemapUrl).GetAttribute<DataContractAttribute>().Name.Should().BeEquivalentTo("url");
        }

        [Fact]
        private void ShouldHaveCorrectDataContractNamespace()
        {
            typeof(SitemapUrl).GetAttribute<DataContractAttribute>()
                              .Namespace.Should()
                              .BeEquivalentTo("http://www.sitemaps.org/schemas/sitemap/0.9");
        }

        [Fact]
        private void ShouldHaveDataContractAttribute()
        {
            typeof(SitemapUrl).Should().BeDecoratedWith<DataContractAttribute>();
        }

        [Fact]
        private void ShouldHaveKnownTypeAttribute()
        {
            typeof(SitemapUrl).GetAttributes<KnownTypeAttribute>().Any().Should().BeTrue();
        }

        [Fact]
        private void ShouldHaveKnownTypeAttributeWithCorrectType()
        {
            typeof(SitemapUrl).GetAttribute<KnownTypeAttribute>().Type.Should().Be<SitemapUrl>();
        }

        public class DataMembers : SitemapUrlTest
        {
            [Theory]
            [InlineData(nameof(SitemapUrl.ChangeFrequency), false)]
            [InlineData(nameof(SitemapUrl.LastModified), false)]
            [InlineData(nameof(SitemapUrl.Location), true)]
            [InlineData(nameof(SitemapUrl.Priority), false)]
            private void ShouldEmitDefault(string propertyName, bool expected)
            {
                typeof(SitemapUrl).GetPropertyByName(propertyName)
                                  .GetCustomAttribute<DataMemberAttribute>()
                                  .EmitDefaultValue
                                  .Should()
                                  .Be(expected);
            }

            [Theory]
            [InlineData(nameof(SitemapUrl.ChangeFrequency), "changefreq")]
            [InlineData(nameof(SitemapUrl.LastModified), "lastmod")]
            [InlineData(nameof(SitemapUrl.Location), "loc")]
            [InlineData(nameof(SitemapUrl.Priority), "priority")]
            private void ShouldHaveCorrectDataMemberName(string propertyName, string dataMemberName)
            {
                typeof(SitemapUrl).GetPropertyByName(propertyName)
                                  .GetCustomAttribute<DataMemberAttribute>()
                                  .Name
                                  .Should()
                                  .BeEquivalentTo(dataMemberName);
            }

            [Theory]
            [InlineData(nameof(SitemapUrl.ChangeFrequency))]
            [InlineData(nameof(SitemapUrl.LastModified))]
            [InlineData(nameof(SitemapUrl.Location))]
            [InlineData(nameof(SitemapUrl.Priority))]
            private void ShouldHaveDataMemberAttribute(string propertyName)
            {
                typeof(SitemapUrl).GetPropertyByName(propertyName)
                                  .Should()
                                  .BeDecoratedWith<DataMemberAttribute>();
            }
        }
    }
}