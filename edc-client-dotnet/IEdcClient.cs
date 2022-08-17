
using edcClientDotnet.model;

namespace edcClientDotnet
{
    // EdcClient is the utility class to get all information about documentation.
    public interface IEdcClient
    {
        /// <summary>
        ///     Create the url for the context according to the main key, the subkey and the language code.
        ///     <p>
        ///     The language code is 2 digits in lowercase ie fr, en, ...
        /// </summary>
        /// <param name="mainKey">the main key</param>
        /// <param name="subKey">the sub key</param>
        /// <param name="languageCode">the language code</param>
        /// <returns>the url</returns>
        /// <exception cref="IOException">if an error is occurred on reading</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String GetContextWebHelpUrl(String mainKey, String subKey, String languageCode);

        /// <summary>
        ///     Create the url for the context according to the main key, the subkey and the language code.
        ///     <p>
        ///     The language code is 2 digits in lowercase ie fr, en, ...
        /// </summary>
        /// <param name="mainKey">the main key</param>
        /// <param name="subKey">the sub key</param>
        /// <param name="rank">the rank</param>
        /// <param name="languageCode">the language code</param>
        /// <returns>the url</returns>
        /// <exception cref="IOException">if an error is occurred on reading</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String GetContextWebHelpUrl(String mainKey, String subKey, int rank, String languageCode);

        /// <summary>
        ///     Create the url for the documentation.
        ///     <p>
        ///     The language code is 2 digits in lowercase ie fr, en, ...
        ///     If languageCode is not defined or not found, system default will be used instead
        ///     Url will include the identifier of publication from where the navigation started, if present
        /// </summary>
        /// <param name="id">the identifier of the documentation</param>
        /// <param name="languageCode">the 2 letters code of the language to use</param>
        /// <param name="srcPublicationId">the identifier of the publication where the navigation starts from</param>
        /// <returns>the url</returns>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String GetDocumentationWebHelpUrl(long id, String languageCode, String srcPublicationId);

        /// <summary>
        ///     Return the context item associated with main and sub keys and the language code.
        /// </summary>
        /// <param name="mainKey">the main key</param>
        /// <param name="subKey">the sub bey</param>
        /// <param name="languageCode">the language code</param>
        /// <returns>the context item</returns>
        /// <exception cref="IOException">if an error is occurred on reading</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        IContextItem GetContextItem(String mainKey, String subKey, String languageCode);

        /// <summary>
        ///     Return the label translation for the given key
        ///     Will read the translated labels from the the i18n files present in the documentation export
        ///     (by default in folder doc/i18n/popover/*.json)
        ///     
        ///     If label was not found in the requested language, it will try and read in the publication default language
        ///     (as defined in the info.json file),
        ///     or in global default labels as a final fallback
        /// </summary>
        /// <param name="labelKey">the label translation key</param>
        /// <param name="languageCode">the 2 letters language code</param>
        /// <param name="publicationId">default language, to use if content was not found in requested language</param>
        /// <returns>the translated label</returns>
        /// <exception cref="IOException">if an error is occurred when reading the files</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String GetLabel(String labelKey, String languageCode, String publicationId);

        /// <summary>
        ///     Return the error translation for the given key
        ///     Will read the translated errors from the the i18n files present in the documentation export
        ///     (by default in folder doc/i18n/popover/*.json)
        ///     
        ///     If error was not found in the requested language, it will try and read in the publication default language
        ///     (as defined in the info.json file),
        ///     or in global default errors as a final fallback
        /// </summary>
        /// <param name="errorKey"></param>
        /// <param name="languageCode"></param>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        String GetError(String errorKey, String languageCode, String publicationId);

        /// <summary>
        ///     Define the server url like http://localhost:8080
        /// </summary>
        /// <param name="serverUrl">the server url</param>
        void SetServerUrl(String serverUrl);

        /// <summary>
        ///     Define the context url ie like doc in http://localhost:8080/doc
        ///     The default value is 'doc'. Do nothing if you don't change the default behavior.
        /// </summary>
        /// <param name="documentationContextUrl">the documentation context url</param>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        void SetDocumentationContextUrl(String documentationContextUrl);

        /// <summary>
        ///     Define the context url ie like help in http://localhost:8080/help
        ///     The default value is 'help'. Do nothing if you don't change the default behavior.
        /// </summary>
        /// <param name="webHelpContextUrl">the web help context url</param>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        void SetWebHelpContextUrl(String webHelpContextUrl);

        /// <summary>
        ///     Force the manager to reload the documentation definition, publication information and translations.
        /// </summary>
        void ForceReload();

        /// <summary>
        ///     Force the documentation loading. This call is optional. Don't call if you want to use the lazy loading.
        /// </summary>
        /// <exception cref="IOException">if an is occurred on reading information</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        void LoadContext();
    }
}