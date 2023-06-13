using System.Collections.ObjectModel;

namespace edcClientDotnet.model
{
    /// This class define a documentation component.
    public interface IDocumentationItem : IObjectId
    {
        /// <summary>
        ///     <para>GET: Return the label</para>
        ///     SET: Define the label
        /// </summary>
        /// <returns>the label</returns>
        String Label { get; set; }

        /// <summary>
        ///     <para>GET: Return the url defined the configuration file (read from a documentation definition).
        ///     It's the real path on the documentation server</para>
        ///     SET: Define the label
        ///     It's the real path on the documentation server.
        /// </summary>
        /// <returns>the url</returns>
        String Url { get; set; }

        /// <summary>
        ///     <para>GET: The publication id.</para>
        ///     SET: Define the publication id
        /// </summary>
        /// <returns>the publicationId</returns>
        String PublicationId { get; set; }

        /// <summary>
        ///     <para>GET: Return the language code of this documentation. The code is defined by 2 digits in lowercase.</para>
        ///     SET: Define the language code of this documentation. The code is defined by 2 digits in lowercase.
        /// </summary>
        /// <returns>the language code</returns>
        String LanguageCode { get; set; }

        /// <summary>
        ///     <para>GET: Return the documentation type.</para>
        ///     SET: Define the documentation type
        /// </summary>
        /// <returns>the documentation type</returns>
        DocumentationItemType DocumentationItemType { get; set; }

        /// <summary>
        ///     <para>Add a documentation item as article.</para>
        ///     Only {@link DocumentationItemType#ARTICLE} is accepted.
        /// </summary>
        /// <param name="article">the article to add</param>
        void AddArticle(IDocumentationItem article);

        /// <summary>
        ///     <para>Return the list of articles.</para>
        ///     This list is unmodifiable.
        /// </summary>
        /// <returns>the list of articles</returns>
        ReadOnlyCollection<IDocumentationItem> GetArticles();

        /// <summary>
        /// Return the number of articles contained by this DocumentationItem
        /// </summary>
        /// <returns>the number of articles</returns>
        int ArticleSize();

        /// <summary>
        ///     <para>Add a documentation item as link.</para>
        ///     All is accepted excepted {@link DocumentationItemType}.
        /// </summary>
        /// <param name="link">the link to add</param>
        void AddLink(IDocumentationItem link);

        /// <summary>
        ///     <para>Return the list of links.</para>
        ///     This list is unmodifiable.
        /// </summary>
        /// <returns>the list of links</returns>
        ReadOnlyCollection<IDocumentationItem> GetLinks();

        /// <summary>
        /// Return the number of links contained by this DocumentationItem
        /// </summary>
        /// <returns>the number of links</returns>
        int LinkSize();
    }
}