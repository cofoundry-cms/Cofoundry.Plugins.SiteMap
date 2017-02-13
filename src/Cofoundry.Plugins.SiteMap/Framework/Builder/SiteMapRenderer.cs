using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// A helper class that renders all registered site map resources
    /// to an xml sitemap string with UTF8 encoding
    /// </summary>
    public class SiteMapRenderer : ISiteMapRenderer
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly ISiteMapBuilderFactory _siteMapBuilderFactory;

        public SiteMapRenderer(
            IQueryExecutor queryExecutor,
            ISiteMapBuilderFactory siteMapBuilderFactory
            )
        {
            _queryExecutor = queryExecutor;
            _siteMapBuilderFactory = siteMapBuilderFactory;
        }

        /// <summary>
        /// Renders all registered site map resources
        /// to an xml sitemap string with UTF8 encoding
        /// </summary>
        public async Task<string> RenderToUTF8StringAsync()
        {
            var siteMapResources = await _queryExecutor.GetAllAsync<ISiteMapResource>();
            var builder = _siteMapBuilderFactory.Create();
            builder.Resources = siteMapResources.ToList();

            return builder.ToString();
        }
    }
}
