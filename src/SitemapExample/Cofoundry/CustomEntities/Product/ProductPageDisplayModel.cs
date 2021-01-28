using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemapExample
{
    public class ProductPageDisplayModel : ICustomEntityPageDisplayModel<ProductDataModel>
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string ShortDescription { get; set; }
    }
}
