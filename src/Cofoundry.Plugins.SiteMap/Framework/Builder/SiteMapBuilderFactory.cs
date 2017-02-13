using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Factory for creating concrete ISiteMapBuilder objects. Override
    /// this implementation to return a custom SiteMapBuilder instance 
    /// if you want more control over how the site map is constructed.
    /// </summary>
    public class SiteMapBuilderFactory : ISiteMapBuilderFactory
    {
        private readonly IResolutionContext _resolutionContext;

        public SiteMapBuilderFactory(
            IResolutionContext resolutionContext
            )
        {
            _resolutionContext = resolutionContext;
        }

        /// <summary>
        /// Creates a new instance of an SiteMapBuilder
        /// </summary>
        public ISiteMapBuilder Create()
        {
            return _resolutionContext.Resolve<SiteMapBuilder>();
        }
    }
}
