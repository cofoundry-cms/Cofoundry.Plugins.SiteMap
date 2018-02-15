using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain;

namespace Cofoundry.Plugins.SiteMap
{
    public class GetAllSiteMapResourcesQueryHandler 
        : IAsyncQueryHandler<GetAllSiteMapResourcesQuery, ICollection<ISiteMapResource>>
        , IIgnorePermissionCheckHandler
    {
        private IEnumerable<ISiteMapResourceRegistration> _siteMapRegistrations;
        private IEnumerable<IAsyncSiteMapResourceRegistration> _asyncSiteMapRegistrations;

        public GetAllSiteMapResourcesQueryHandler(
            IEnumerable<ISiteMapResourceRegistration> siteMapRegistrations,
            IEnumerable<IAsyncSiteMapResourceRegistration> asyncSiteMapRegistrations
            )
        {
            _siteMapRegistrations = siteMapRegistrations;
            _asyncSiteMapRegistrations = asyncSiteMapRegistrations;
        }

        public async Task<ICollection<ISiteMapResource>> ExecuteAsync(GetAllSiteMapResourcesQuery query, IExecutionContext executionContext)
        {
            var allResources = new List<ISiteMapResource>();

            foreach (var registration in _siteMapRegistrations)
            {
                var resources = registration.GetResources();
                allResources.AddRange(resources);
            }

            foreach (var registration in _asyncSiteMapRegistrations)
            {
                var resources = await registration.GetResourcesAsync();
                allResources.AddRange(resources);
            }

            return allResources;
        }
    }
}
