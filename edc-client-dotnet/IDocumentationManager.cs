using edc_client_dotnet.model;

namespace edc_client_dotnet
{
    /// DocumentationManager manage all content of the documentation.
    public interface IDocumentationManager
    {
        /// <summary>
        ///     Get the context item according to the keys and the language or the default language if not found.
        ///     If the context was not found in the requested language, it will find the publication from the given keys
        ///     and use the default language, identified from the the default languages dictionary
        /// </summary>
        /// <param name="mainKey">the main key</param>
        /// <param name="subKey">the sub key</param>
        /// <param name="languageCode">the language code</param>
        /// <param name="defaultLanguages">a dictionary containing the publication id as key and default language code as value</param>
        /// <returns>the context item or null</returns>
        /// <exception cref="IOException">if an error is occurred on reading</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        IContextItem GetContext(String mainKey, String subKey, String languageCode, IReadOnlyDictionary<String, String> defaultLanguages);

        /// <summary>
        ///     Force the reload of the documentation definition on the next call.
        /// </summary>
        void ForceReload();

        /// <summary>
        ///     Force the documentation loading. This call is optional. Don't call if you want to use the lazy loading.
        /// </summary>
        /// <exception cref="IOException">if an error is occurred on reading</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        void LoadContext(); 
    }
}
