using edcClientDotnet.model;
using NLog;

namespace edcClientDotnet.internalImpl.model
{
    public class I18NContentImpl : II18NContent
    {
        private readonly Dictionary<String, String> _translation = new();
        private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        public string GetTranslation(string lang, string type, string key, string publicationId)
        {
            _logger.Debug("Get the translated content for the lang: {}, type: {}, key: {}, value: {}", lang, type, key);
            if(_translation.ContainsKey(lang + "." + type + "." + key)) {
                return _translation[lang + "." + type + "." + key];
            } else
            {
                return null;
            }
            
        }

        public void SetTranslation(string lang, string type, string key, string value)
        {
            _logger.Debug("Set the translated content for the lang: {}, type: {}, key: {}, value: {}", lang, type, key, value);
            _translation.Add(lang + "." + type + "." + key, value);
        }
    }
}
