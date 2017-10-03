using System.Runtime.Serialization;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    [DataContract(Name = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [KnownType(typeof(SitemapUrl))]
    public sealed class SitemapUrl
    {
        [DataMember(Name = "changefreq", EmitDefaultValue = false)]
        public ChangeFrequency? ChangeFrequency { get; set; }

        [DataMember(Name = "lastmod", EmitDefaultValue = false)]
        public string LastModified { get; set; }

        [DataMember(Name = "loc")]
        public string Location { get; set; }

        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public decimal? Priority { get; set; }
    }
}