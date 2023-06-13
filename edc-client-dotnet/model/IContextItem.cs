namespace edcClientDotnet.model
{
    // This class is a specialized DocumentationItem for the contextual content (bricks)
    public interface IContextItem : IDocumentationItem
    {
        /// <summary>
        ///     <para>GET: Return the description.</para>
        ///     SET: Define the description
        /// </summary>
        /// <returns>the description</returns>
        String Description { get; set; }

        /// <summary>
        ///     <para>Return the main key pluginId, package, other, ...</para>
        ///     SET: Define the main key
        /// </summary>
        /// <returns>the main key</returns>
        String MainKey { get; set; }

        /// <summary>
        ///     <para>GET: Return the secondary key (id of the brick)</para>
        ///     SET: Define the secondary key
        /// </summary>
        /// <returns>the secondary key</returns>
        String SubKey { get; set; }
    }
}
