using edcClientDotnet.model;

namespace edcClientDotnet.io
{
    public interface IEdcReader
    {
        /// <summary>
        ///     Read all context items and associate it in the Dictionary.
        ///     The key of the Dictionary is the contextual key and he value is the associated context.
        /// </summary>
        /// <returns>a dictionary which associate the contextual key with the content item.</returns>
        /// <exception cref="IOException">if an IO error occurred during the read</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        Dictionary<String, IContextItem> GetContext();

        /// <summary>
        ///     Read the export information, for every publication, from the info.json files
        ///     The key of the returned dictionary is the publication id and the value the associated information object
        /// </summary>
        /// <returns>a dictionary containing the keys and label translations associated</returns>
        /// <exception cref="IOException">if an IO error occurred during the read</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        Dictionary<String, IInformation> GetInformations();

        /// <summary>
        ///     Read the translated popover label for the given language code
        /// </summary>
        /// <param name="languagesCodes"></param>
        /// <returns>an object</returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        II18NContent ReadLabel(HashSet<String> languagesCodes);
    }
}
