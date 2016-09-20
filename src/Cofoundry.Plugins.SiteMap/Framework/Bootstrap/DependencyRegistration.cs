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
                .RegisterType<SiteMapBuilder, SiteMapBuilder>(new RegistrationOptions() { InstanceScope = InstanceScope.Transient })
                .RegisterType<ISiteMapBuilderFactory, SiteMapBuilderFactory>()
                .RegisterType<ISiteMapRenderer, SiteMapRenderer>()

                .RegisterAll<ISiteMapResourceRegistration>()
                .RegisterAll<IAsyncSiteMapResourceRegistration>()
                ;
        }
    }
}
