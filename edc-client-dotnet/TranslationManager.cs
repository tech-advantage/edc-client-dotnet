using edc_client_dotnet.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet
{
    public interface TranslationManager
    {
        /// <summary>
        ///     Load the translation information and the translation popover labels
        ///     
        ///     Information :  - load the languages used by all the publications
        ///                    - load the map of default languages by publication
        ///     Labels: the labels used by the popover (articles, links),
        ///     These labels are read from the i18n files present in the documentation root folder
        ///     
        /// </summary>
        /// <param name="publicationInformation">a map containing all the publication information for every publication</param>
        /// <exception cref="IOException">if an IO error occurred</exception>
        /// <exception cref="InvalidUrlException">if the i18n files urls were not valid</exception>
        void LoadTranslations(Dictionary<String, IInformationService> publicationInformation);

        /// <summary>
        ///     Force the reload of the translation information and labels on the next read
        /// </summary>
        void ForceReload();

        /// <summary>
        ///     Return the translated popover label for the requested key
        ///     If no label was found for the language, will look for the label in the publication default language
        ///     Finally, system default labels will be used if no valid label was found in default language
        /// </summary>
        /// <param name="labelKey">the label key</param>
        /// <param name="languageCode">the requested language</param>
        /// <param name="publicationId">the identifier of the publication, to find the default language</param>
        /// <returns>A string containing the translated label</returns>
        String GetLabel(String labelKey, String languageCode, String publicationId);

        /// <summary>
        ///     Return the translated popover error for the requested key
        ///     If no error was found for the language, will look for the error in the publication default language
        ///     Finally, system default errors will be used if no valid error was found in default language
        /// </summary>
        /// <param name="errorKey"></param>
        /// <param name="languageCode"></param>
        /// <param name="publicationId"></param>
        /// <returns>A string containing the translated error</returns>
        String GetError(String errorKey, String languageCode, String publicationId);

        /// <summary>
        ///     Return a Map with the default language for each publication
        /// </summary>
        /// <returns>a Map containing the publication id as key, default language code as value</returns>
        Dictionary<String, String> GetDefaultPublicationLangugages();
    }
}
