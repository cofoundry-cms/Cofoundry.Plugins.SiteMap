namespace Cofoundry.Plugins.SiteMap;

/// <summary>
/// Factory for creating concrete ISiteMapBuilder objects.
/// </summary>
public interface ISiteMapBuilderFactory
{
    /// <summary>
    /// Creates a new instance of an ISiteMapBuilder
    /// </summary>
    ISiteMapBuilder Create();
}
