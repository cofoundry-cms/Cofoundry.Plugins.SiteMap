﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain;

namespace Cofoundry.Plugins.SiteMap
{
    public class GetAllSiteMapResourcesQueryHandler 
        : IAsyncQueryHandler<GetAllQuery<SiteMapResource>, IEnumerable<SiteMapResource>>
        , IIgnorePermissionCheckHandler
    {
        private ISiteMapResourceRegistration[] _siteMapRegistrations;
        private IAsyncSiteMapResourceRegistration[] _asyncSiteMapRegistrations;

        public GetAllSiteMapResourcesQueryHandler(
            ISiteMapResourceRegistration[] siteMapRegistrations,
            IAsyncSiteMapResourceRegistration[] asyncSiteMapRegistrations
            )
        {
            _siteMapRegistrations = siteMapRegistrations;
            _asyncSiteMapRegistrations = asyncSiteMapRegistrations;
        }

        public async Task<IEnumerable<SiteMapResource>> ExecuteAsync(GetAllQuery<SiteMapResource> query, IExecutionContext executionContext)
        {
            var allResources = new List<SiteMapResource>();

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
