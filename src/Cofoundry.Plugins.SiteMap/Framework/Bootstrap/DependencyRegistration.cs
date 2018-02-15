using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Plugins.SiteMap
{
    public class DependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<ISiteMapBuilderFactory, SiteMapBuilderFactory>()
                .Register<ISiteMapRenderer, SiteMapRenderer>()

                .RegisterAll<ISiteMapResourceRegistration>()
                .RegisterAll<IAsyncSiteMapResourceRegistration>()
                ;
        }
    }
}
