using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.SiteMap
{
    public interface ISiteMapBuilderFactory
    {
        ISiteMapBuilder Create();
    }
}
