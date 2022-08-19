using edcClientDotnet.model;
using edcClientDotnet.utils;
using static edcClientDotnet.model.I18NTranslation;

namespace edcClientDotnet.internalImpl.model
{

    public class InformationImpl : IInformation
    {
        private String _defaultLanguage = ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE);
        private HashSet<String> _languages;

        public String DefaultLanguage {
            get => _defaultLanguage;
            set => _defaultLanguage = value;
        }
        
        public HashSet<String> Languages {
            get => _languages;
            set => _languages = value;
        }
    }
}
