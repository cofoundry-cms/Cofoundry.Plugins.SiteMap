using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Resgitration for resources that should appear in an xml site map file
    /// </summary>
    public interface IAsyncSiteMapResourceRegistration
    {
        Task<IEnumerable<SiteMapResource>> GetResourcesAsync();
    }
}
