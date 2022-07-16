using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class I18NContentService : II18NContentService
    {
        Dictionary<String, String> translation = new Dictionary<String, String>();
        private Dictionary<String, String> i18nLabels = new Dictionary<String, String>();
        private Dictionary<String, String> i18nErrors = new Dictionary<String, String>();

        public string GetTranslation(string lang, string type, string key, string publicationId)
        {
            return translation.GetValueOrDefault(lang + "." + type + "." + key);
        }       

        public void SetMessage(string lang, string type, string key, string value)
        {
            translation.Add(lang + "." + type + "." + key, value);
        }

        public void SetI18nLabel(Dictionary<string, string> i18nLabel)
        {
            foreach (var content in i18nLabel)
            {
                i18nLabels.Add(content.Key, content.Value);
            }
        }

        public void SetI18nError(Dictionary<string, string> i18nError)
        {
            foreach (var content in i18nError)
            {
                i18nErrors.Add(content.Key, content.Value);
            }
        }

        public Dictionary<string, string> GetLabel()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetError()
        {
            throw new NotImplementedException();
        }
    }
}
