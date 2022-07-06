using edc_client_dotnet.model;

namespace edc_client_dotnet.io
{
    internal interface IEdcReader
    {
        /// <summary>
        ///     Read all context items and associate it in the map.
        ///     The key of the map is the contextual key and he value is the associated context.
        /// </summary>
        /// <returns>a map which associate the contextual key with the content item.</returns>
        Dictionary<String, IContextItem> ReadContext();

        /// <summary>
        ///     Read the export information, for every publication, from the info.json files
        ///     The key of the returned map is the publication id and the value the associated information object
        /// </summary>
        /// <returns>a map containing the keys and label translations associated</returns>
        Dictionary<String, IInformation> ReadInfo();

        /// <summary>
        ///     Read the translated popover label for the given language code
        /// </summary>
        /// <param name="languagesCodes"></param>
        /// <returns></returns>
        II18NContent ReadLabel(HashSet<String> languagesCodes);
    }
}
