using edcClientDotnet.model;
using edcClientDotnet.utils;
using static edcClientDotnet.model.I18NTranslation;

namespace edcClientDotnet.internalImpl.model
{

    public class InformationImpl : IInformation
    {
        private String _defaultLanguage = ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE);
        private HashSet<String> _languages;

        public String GetDefaultLanguage() { return _defaultLanguage; }
        
        public HashSet<String> GetLanguages() { return _languages; }

        public void SetDefaultLanguage(String defaultLanguage) { _defaultLanguage = defaultLanguage; }

        public void SetLanguages(HashSet<String> languages) { _languages = languages; }
    }
}
