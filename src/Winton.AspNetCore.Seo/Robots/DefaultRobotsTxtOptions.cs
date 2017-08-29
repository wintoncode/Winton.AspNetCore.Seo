using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Winton.AspNetCore.Seo.Robots
{
    internal sealed class DefaultRobotsTxtOptions : IRobotsTxtOptions
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public DefaultRobotsTxtOptions(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public bool AddSitemapUrl => true;

        public IEnumerable<UserAgentRecord> UserAgentRecords => new List<UserAgentRecord>
        {
            new UserAgentRecord
            {
                DisallowAll = !_hostingEnvironment.IsProduction()
            }
        };
    }
}