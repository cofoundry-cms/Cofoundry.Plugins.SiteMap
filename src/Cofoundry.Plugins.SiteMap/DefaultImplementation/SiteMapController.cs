using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Plugins.SiteMap
{
    public class SiteMapController : Controller
    {
        #region Constructors

        private readonly ISiteMapRenderer _siteMapRenderer;

        public SiteMapController(
            IQueryExecutor queryExecutor,
            ISiteMapRenderer siteMapRenderer
            )
        {
            _siteMapRenderer = siteMapRenderer;
        }

        #endregion

        [Route("sitemap.xml")]
        public async Task<ContentResult> SitemapXml()
        {
            var siteMap = await _siteMapRenderer.RenderToUTF8StringAsync();
            return Content(siteMap, "application/xml", Encoding.UTF8);
        }

        [Route("sitemap.xsl")]
        public ContentResult SitemapXsl()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Cofoundry.Plugins.SiteMap.DefaultImplementation.sitemap.xsl"))
            using(StreamReader reader = new StreamReader(stream))
            {
                return Content(reader.ReadToEnd(), "application/xml", Encoding.UTF8);
            }
        }
    }
}
