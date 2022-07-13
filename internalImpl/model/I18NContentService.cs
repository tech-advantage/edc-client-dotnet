using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class I18NContentService : II18NContentService
    {
        Dictionary<String, String> translation = new Dictionary<String, String>();
        public string GetTranslation(string lang, string type, string key, string publicationId)
        {
            return translation.GetValueOrDefault(lang + "." + type + "." + key);
            
        }

        public void SetMessage(string lang, string type, string key, string value)
        {
            translation.Add(lang + "." + type + "." + key, value);
        }
    }
}
