using edcClientDotnet.model;

namespace edcClientDotnet.utils
{
    public interface ITranslationUtil
    {
        /// <summary>
        ///     Return a Dictionary of default languages by publication id
        /// </summary>
        /// <param name="information">the Dictionary containing the publication id as key and information as value</param>
        /// <returns>a Dictionary of publication id as key and default content language code as value</returns>
        SortedDictionary<String, String> GetPublicationDefaultLanguages(SortedDictionary<String, IInformation> information);

        /// <summary>
        ///     Check labels consistency
        ///     Labels must contain all the keys present in default language labels
        ///     And their values must not be empty
        /// </summary>
        /// <param name="labels">the labels to check</param>
        /// <returns>true if labels are valid</returns>
        Boolean CheckTranslatedLabels(Dictionary<String, String> labels);

        /// <summary>
        ///    Return true if given language code is present in the list of the codes used in the application 
        /// </summary>
        /// <param name="languageCode">the language code to check</param>
        /// <returns>true if code is present</returns>
        Boolean IsLanguageCodeValid(String languageCode);
    }
}
