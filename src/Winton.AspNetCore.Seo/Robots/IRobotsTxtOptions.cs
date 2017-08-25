using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.Robots
{
    public interface IRobotsTxtOptions
    {
        bool AddSitemapUrl { get; }

        IEnumerable<UserAgentRecord> UserAgentRecords { get; }
    }
}