using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cofoundry.Plugins.SiteMap
{
    /// <summary>
    /// Builds a site map xml file from a set of resources. Call ToString()
    /// to render the xml file to a UTF8 string.
    /// </summary>
    public interface ISiteMapBuilder
    {
        /// <summary>
        /// The resources to include in the site map. These will be autiomatically
        /// ordered by priority when rendering.
        /// </summary>
        List<ISiteMapResource> Resources { get; set; }

        /// <summary>
        /// Creates the SiteMap xml document.
        /// </summary>
        XDocument ToXml();
    }
}
