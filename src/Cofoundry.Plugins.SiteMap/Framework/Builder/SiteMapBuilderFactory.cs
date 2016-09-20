using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Plugins.SiteMap
{
    public class SiteMapBuilderFactory : ISiteMapBuilderFactory
    {
        private readonly IResolutionContext _resolutionContext;

        public SiteMapBuilderFactory(
            IResolutionContext resolutionContext
            )
        {
            _resolutionContext = resolutionContext;
        }

        public ISiteMapBuilder Create()
        {
            return _resolutionContext.Resolve<SiteMapBuilder>();
        }
    }
}
