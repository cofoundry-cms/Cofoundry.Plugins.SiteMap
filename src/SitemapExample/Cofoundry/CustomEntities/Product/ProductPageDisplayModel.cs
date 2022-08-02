namespace SitemapExample;

public class ProductPageDisplayModel : ICustomEntityPageDisplayModel<ProductDataModel>
{
    public string PageTitle { get; set; }

    public string MetaDescription { get; set; }

    public string ShortDescription { get; set; }
}
