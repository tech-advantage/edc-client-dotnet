namespace edc_client_dotnet.utils
{
    public interface IUrlUtil
    {
        /// <summary>
        ///     Return the home url for the help client
        /// </summary>
        /// <returns>the home url</returns>
        /// <exception cref="InvalidUrlException">If the base url is not defined</exception>
        String GetHomeUrl();

        /// <summary>
        ///     Return the error url for the help client
        /// </summary>
        /// <returns>the home url</returns>
        /// <exception cref="InvalidUrlException">If the base url is not defined</exception>
        String GetErrorUrl();

        /// <summary>
        ///     Build the web help context url for the brick defined with the keys, the language and the index of the article to display
        /// </summary>
        /// <param name="publicationId">the identifier of the publication</param>
        /// <param name="mainKey">the main key</param>
        /// <param name="subKey">the sub key</param>
        /// <param name="languageCode">the language code</param>
        /// <param name="articleIndex">the article index to display</param>
        /// <returns>the url</returns>
        /// <exception cref="InvalidUrlException">If the url is malformed</exception>
        String GetContextUrl(String publicationId, String mainKey, String subKey, String languageCode, int articleIndex);

        /// <summary>
        ///     Build the web help documentation url for the document defined by the identifier, the language code, and the publication identifier if present
        /// </summary>
        /// <param name="id">the idenitifer of the documentation</param>
        /// <param name="languageCode">the 2 letters code of the language (en, fr..)</param>
        /// <param name="srcPublicationId">the identifier of the publication from where navigation will start</param>
        /// <returns>the url</returns>
        /// <exception cref="InvalidUrlException">If the url is malformed</exception>
        String GetDocumentationUrl(long id, String languageCode, String srcPublicationId);
    }
}
