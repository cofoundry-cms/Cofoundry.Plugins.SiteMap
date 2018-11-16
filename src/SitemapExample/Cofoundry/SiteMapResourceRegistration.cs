using Cofoundry.Plugins.SiteMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemapExample.Cofoundry
{
    public class SiteMapResourceRegistration : ISiteMapResourceRegistration
    {
        public IEnumerable<ISiteMapResource> GetResources()
        {
            yield return new SiteMapResource()
            {
                Url = "/test",
                LastModifiedDate = DateTime.UtcNow,
                Priority = 0.7m
            };
        }
    }
}
