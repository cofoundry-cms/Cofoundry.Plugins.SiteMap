using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Resgitration for resources that should appear in the xml site map file.
    /// </summary>
    public class CofoundryContentSiteMapResourceRegistration : IAsyncSiteMapResourceRegistration
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IPermissionValidationService _permissionValidationService;

        public CofoundryContentSiteMapResourceRegistration(
            IQueryExecutor queryExecutor,
            IPermissionValidationService permissionValidationService
            )
        {
            _queryExecutor = queryExecutor;
            _permissionValidationService = permissionValidationService;
        }

        public async Task<IEnumerable<SiteMapResource>> GetResourcesAsync()
        {
            var resources = new List<SiteMapResource>();
            if (_permissionValidationService.HasPermission<PageReadPermission>()) return resources;

            var pageRoutes = await _queryExecutor.GetAllAsync<PageRoute>();
            var allRules = _queryExecutor.GetAll<ICustomEntityRoutingRule>();

            foreach (var pageRoute in pageRoutes.Where(p => p.IsPublished && p.ShowInSiteMap))
            {
                if (pageRoute.PageType == PageType.CustomEntityDetails)
                {
                    if (_permissionValidationService.HasCustomEntityPermission<CustomEntityReadPermission>(pageRoute.CustomEntityDefinitionCode))
                    {
                        var routesQuery = new GetCustomEntityRoutesByDefinitionCodeQuery(pageRoute.CustomEntityDefinitionCode);
                        var allCustomEntityRoutes = await _queryExecutor.ExecuteAsync(routesQuery);
                        var pageLocaleId = pageRoute.Locale != null ? pageRoute.Locale.LocaleId : (int?)null;

                        foreach (var customEntityRoute in allCustomEntityRoutes
                            .Where(r => r.Locale == null ? !pageLocaleId.HasValue : r.Locale.LocaleId == pageLocaleId))
                        {
                            var resource = MapCustomEntityResource(pageRoute, customEntityRoute, allRules);
                            if (resource != null)
                            {
                                resources.Add(resource);
                            }
                        }
                    }
                }
                else
                {
                    resources.Add(MapPageResource(pageRoute));
                }
            }

            return resources;
        }

        private SiteMapResource MapCustomEntityResource(PageRoute pageRoute, CustomEntityRoute customEntityRoute, IEnumerable<ICustomEntityRoutingRule> allRules)
        {
            var version = customEntityRoute.Versions.GetVersionRouting(WorkFlowStatusQuery.Published);
            if (version == null) return null;

            var rule = allRules.FirstOrDefault(r => r.RouteFormat == pageRoute.UrlPath);
            if (rule == null) return null;

            var resource = new SiteMapResource();
            resource.Url = rule.MakeUrl(pageRoute, customEntityRoute);
            // TODO: this would better be a publish date for the custom entity
            resource.LastModifiedDate = version.CreateDate;
            resource.Priority = GetPriority(pageRoute);

            return resource;
        }

        private SiteMapResource MapPageResource(PageRoute pageRoute)
        {
            var version = pageRoute.Versions.GetVersionRouting(WorkFlowStatusQuery.Published);
            var resource = new SiteMapResource();
            resource.Url = pageRoute.FullPath;
            // TODO: this would better be a publish date for the page
            resource.LastModifiedDate = version.CreateDate;
            resource.Priority = GetPriority(pageRoute);

            return resource;
        }

        private decimal GetPriority(PageRoute pageRoute)
        {
            var isDirectoryDefaultPage = pageRoute.IsDirectoryDefaultPage();
            var isInSiteRoot = pageRoute.WebDirectory.IsSiteRoot();

            // Site/Language root
            if (isDirectoryDefaultPage && isDirectoryDefaultPage) return 1;
            // Directory root
            if (isDirectoryDefaultPage) return 0.8m;

            // Directory sub pages
            return 0.6m;
        }
    }
}
