using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winton.AspNetCore.Seo.Robots
{
    public sealed class UserAgentRecord
    {
        public IEnumerable<string> Disallow { get; set; }

        public bool DisallowAll { get; set; }

        public UserAgent UserAgent { get; set; } = UserAgent.Any;

        internal string CreateRecord()
        {
            StringBuilder stringBuilder = new StringBuilder()
                .AppendLine($"User-agent: {UserAgent}");

            IEnumerable<string> disallowedUrls = DisallowAll
                ? new List<string> { "/" }
                : (Disallow ?? Enumerable.Empty<string>()).DefaultIfEmpty();

            foreach (string url in disallowedUrls)
            {
                stringBuilder.AppendLine($"Disallow: {url}");
            }

            return stringBuilder.ToString();
        }
    }
}