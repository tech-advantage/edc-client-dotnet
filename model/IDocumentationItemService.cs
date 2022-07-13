using System.Collections.ObjectModel;


namespace edc_client_dotnet.model
{
    public interface IDocumentationItemService : IObjectIdService
    {

        /// <summary>
        ///     Return the label
        /// </summary>
        /// <returns>the label</returns>
        String GetLabel();
        
        /// <summary>
        ///     Define the label
        /// </summary>
        /// <param name="label">label the label to set</param>
        void SetLabel(String label);

        /// <summary>
        ///     Return the url defined the configuration file (read from a documentation definition).
        ///     It's the real path on the documentation server.
        /// </summary>
        /// <returns>the url</returns>
        String GetUrl();

        /// <summary>
        ///     Define the url.
        ///     It's the real path on the documentation server.
        /// </summary>
        /// <param name="url">url the url to set</param>
        void SetUrl(String url);

        /// <summary>
        ///     Get the publication id.
        /// </summary>
        /// <returns>the publicationId</returns>
        String GetPublicationId();

        /// <summary>
        ///     Define the publication id
        /// </summary>
        /// <param name="publicationId">publicationId the identifier of the publication</param>
        void SetPublicationId(String publicationId);

        /// <summary>
        ///     Return the language code of this documentation.
        ///     <p>
        ///     The code is defined by 2 digits in lowercase.
        /// </summary>
        /// <returns>the language code</returns>
        String GetLanguageCode();

        /// <summary>
        ///     Define the language code of this documentation.
        ///     <p>
        ///     The code is defined by 2 digits in lowercase.
        /// </summary>
        /// <param name="languageCode">the language code to set</param>
        void SetLanguageCode(String languageCode);

        /// <summary>
        ///     Return the documentation type.
        /// </summary>
        /// <returns>the documentation type</returns>
        DocumentationItemType GetDocumentationItemType();
 
        /// <summary>
        ///     Define the documentation type
        /// </summary>
        /// <param name="documentationItemType">documentationItemType the documentation type to set</param>
        void SetDocumentationItemType(DocumentationItemType documentationItemType);

        /// <summary>
        ///     Add a documentation item as article.
        ///     <p>
        ///     Only {@link DocumentationItemType#ARTICLE} is accepted.
        /// </summary>
        /// <param name="article">Article the article to add</param>
        void AddArticle(IDocumentationItemService article);

        /// <summary>
        ///     Return the list of articles.
        ///     <p>
        ///     This list is unmodifiable.
        /// </summary>
        /// <returns>The list of articles</returns>
        ReadOnlyCollection<IDocumentationItemService> GetArticles();

        /// <summary>
        ///     Return the number of articles contained by this DocumentationItem
        /// </summary>
        /// <returns>The number of articles</returns>
        int ArticleSize();

        /// <summary>
        ///     Add a documentation item as link.
        ///     <p>
        ///     All is accepted excepted {@link DocumentationItemType}.
        /// </summary>
        /// <param name="link">the link to add</param>
        void AddLink(IDocumentationItemService link);

        /// <summary>
        ///     Return the list of links.
        ///     <p>
        ///     This list is unmodifiable.
        /// </summary>
        /// <returns>The list of links</returns>
        ReadOnlyCollection<IDocumentationItemService> GetLinks();

        /// <summary>
        ///     Return the number of links contained by this DocumentationItem
        /// </summary>
        /// <returns>The number of links</returns>
        int LinkSize();
    }
}