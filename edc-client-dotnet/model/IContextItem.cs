
namespace edcClientDotnet.model
{
    // This class is a specialized DocumentationItem for the contextual content (bricks)
    public interface IContextItem : IDocumentationItem
    {

        /// <summary>
        ///     Return the description
        /// </summary>
        /// <returns>the description</returns>
        String GetDescription();

        /// <summary>
        ///     Define the description
        /// </summary>
        /// <param name="description">the description to set</param>
        void SetDescription(String description);

        /// <summary>
        ///     Return the main key pluginId, package, other, ...
        /// </summary>
        /// <returns>the main key</returns>
        String GetMainKey();

        /// <summary>
        ///     Define the main key
        /// </summary>
        /// <param name="mainKey">the main key to set</param>
        void SetMainKey(String mainKey);

        /// <summary>
        ///     Return the secondary key (id of the brick)
        /// </summary>
        /// <returns>the secondary key</returns>
        String GetSubKey();

        /// <summary>
        ///     Define the secondary key
        /// </summary>
        /// <param name="subKey">the secondary key to set</param>
        void SetSubKey(String subKey);
    }
}
