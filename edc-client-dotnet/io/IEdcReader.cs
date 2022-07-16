﻿using edc_client_dotnet.model;

namespace edc_client_dotnet.io
{
    public interface IEdcReader
    {
        /// <summary>
        ///     Read all context items and associate it in the map.
        ///     The key of the map is the contextual key and he value is the associated context.
        /// </summary>
        /// <returns>a map which associate the contextual key with the content item.</returns>
        /// <exception cref="IOException">if an IO error occurred during the read</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        //Dictionary<String, IContextItemService> ReadContext();

        /// <summary>
        ///     Read the export information, for every publication, from the info.json files
        ///     The key of the returned map is the publication id and the value the associated information object
        /// </summary>
        /// <returns>a map containing the keys and label translations associated</returns>
        /// <exception cref="IOException">if an IO error occurred during the read</exception>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        Dictionary<String, IInformationService> ReadInfo();

        /// <summary>
        ///     Read the translated popover label for the given language code
        /// </summary>
        /// <param name="languagesCodes"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        //II18NContentService ReadLabel(HashSet<String> languagesCodes);
    }
}