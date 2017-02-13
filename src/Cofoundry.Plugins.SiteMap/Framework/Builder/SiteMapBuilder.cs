using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cofoundry.Core.Web;
using Cofoundry.Core.Validation;
using Cofoundry.Core.IO;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Builds a site map xml file from a set of resources. Call ToString()
    /// to render the xml file to a UTF8 string.
    /// </summary>
    public class SiteMapBuilder : ISiteMapBuilder
    {
        #region constructor

        private readonly ISiteUrlResolver _uriResolver;
        private readonly IModelValidationService _modelValidationService;

        public SiteMapBuilder(
            ISiteUrlResolver uriResolver,
            IModelValidationService modelValidationService
            )
        {
            _uriResolver = uriResolver;
            _modelValidationService = modelValidationService;
        }

        #endregion

        #region public

        /// <summary>
        /// The resources to include in the site map. These will be autiomatically
        /// ordered by priority when rendering.
        /// </summary>
        public List<ISiteMapResource> Resources { get; set; }

        /// <summary>
        /// Creates the SiteMap xml document.
        /// </summary>
        public XDocument ToXml()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"sitemap.xsl\""));

            var urlset = new XElement(ns + "urlset",
                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                        new XAttribute(xsi + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"));

            doc.Add(urlset);

            foreach (var resource in Resources
                .OrderByDescending(p => p.Priority)
                .ThenBy(p => p.Url))
            {
                _modelValidationService.Validate(resource);
                var el = new XElement(ns + "url", new XElement(ns + "loc", _uriResolver.MakeAbsolute(resource.Url)));
                
                if (resource.LastModifiedDate.HasValue)
                {
                    el.Add(new XElement(ns + "lastmod", resource.LastModifiedDate.Value.ToString("yyyy-MM-dd")));
                }

                if (resource.Priority.HasValue)
                {
                    el.Add(new XElement(ns + "priority", resource.Priority.Value.ToString("0.0")));
                }

                urlset.Add(el);
            }

            return doc;
        }

        /// <summary>
        /// Renders the builder to a string with UTF8 encoding.
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            var doc = ToXml();
            using (var writer = new Utf8StringWriter(builder))
            {
                doc.Save(writer, SaveOptions.None);
            }
            return builder.ToString();
        }

        #endregion
    }
}
