using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class I18NContentImpl : II18NContent
    {
        Dictionary<String, String> _translation = new Dictionary<String, String>();
        private Dictionary<String, String> _i18nLabels = new Dictionary<String, String>();
        private Dictionary<String, String> _i18nErrors = new Dictionary<String, String>();
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public String GetTranslation(String lang, String type, String key, String publicationId)
        {
            _logger.Debug("Get translation lang: {}, type: {}, key: {}, publicationId: {} ", lang, type, key, publicationId);
            return _translation.GetValueOrDefault(lang + "." + type + "." + key, "");
        }       

        public void SetMessage(String lang, String type, String key, String value)
        {
            _logger.Debug("Set Message traduction lang: {}, type: {}, key: {}, value: {} ", lang, type, key, value);
            _translation.Add(lang + "." + type + "." + key, value);
        }

        public void SetI18nLabel(Dictionary<String, String> i18nLabel)
        {
            foreach (var content in i18nLabel)
            {
                _i18nLabels.Add(content.Key, content.Value);
            }
        }

        public void SetI18nError(Dictionary<String, String> i18nError)
        {
            foreach (var content in i18nError)
            {
                _i18nErrors.Add(content.Key, content.Value);
            }
        }
    }
}
