using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Plugins.SiteMap
{
    public class GetAllSiteMapResourcesQueryHandler
        : IQueryHandler<GetAllSiteMapResourcesQuery, ICollection<ISiteMapResource>>
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