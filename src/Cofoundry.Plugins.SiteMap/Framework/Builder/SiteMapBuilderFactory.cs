using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Factory for creating concrete ISiteMapBuilder objects. Override
    /// this implementation to return a custom SiteMapBuilder instance 
    /// if you want more control over how the site map is constructed.
    /// </summary>
    public class SiteMapBuilderFactory : ISiteMapBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SiteMapBuilderFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates a new instance of an SiteMapBuilder
        /// </summary>
        public ISiteMapBuilder Create()
        {
            return _serviceProvider.GetRequiredService<SiteMapBuilder>();
        }
    }
}
