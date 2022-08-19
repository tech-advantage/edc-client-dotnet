using edcClientDotnet.model;
using edcClientDotnet.utils;
using static edcClientDotnet.model.I18NTranslation;

namespace edcClientDotnet.internalImpl.util
{
    public class TranslationUtilImpl : ITranslationUtil
    {
        public SortedDictionary<String, String> GetPublicationDefaultLanguages(SortedDictionary<String, IInformation> information)
        {
            SortedDictionary<String, String> languages = new();
           
            foreach(var language in information)
            {
                languages.Add(language.Key, GetDefaultLanguage(language.Value));
            }

            return languages;
        }

        public bool CheckTranslatedLabels(Dictionary<String, String> labels)
        {
            
            return labels != null && labels.Any()
                && TranslationConstants.GetDefaultLabels().ToList().TrueForAll(e => labels.ContainsKey(e.Key))
                && labels.Values.ToList().TrueForAll(e => !String.IsNullOrEmpty(e));
        }

        public bool IsLanguageCodeValid(String languageCode)
        {
            return TranslationConstants.LANGUAGES_CODES.Contains(languageCode);
        }

        private String GetDefaultLanguage(IInformation info)
        {
            return (info != null && String.IsNullOrEmpty(info.DefaultLanguage)) ? 
                ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE) : info.DefaultLanguage;
        }
    }
}
