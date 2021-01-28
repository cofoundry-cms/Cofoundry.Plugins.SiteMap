using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemapExample
{
    public class ProductPageDisplayModelMapper : ICustomEntityDisplayModelMapper<ProductDataModel, ProductPageDisplayModel>
    {
        public Task<ProductPageDisplayModel> MapDisplayModelAsync(
            CustomEntityRenderDetails renderDetails, 
            ProductDataModel dataModel, 
            PublishStatusQuery publishStatusQuery
            )
        {
            var displayModel = new ProductPageDisplayModel()
            {
                PageTitle = renderDetails.Title,
                MetaDescription = dataModel.ShortDescription,
                ShortDescription = dataModel.ShortDescription
            };

            return Task.FromResult(displayModel);
        }
    }
}
