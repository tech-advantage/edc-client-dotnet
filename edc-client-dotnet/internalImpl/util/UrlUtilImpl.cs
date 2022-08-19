using edcClientDotnet.model;
using edcClientDotnet.utils;

namespace edcClientDotnet.internalImpl.util
{
    public class UrlUtilImpl : IUrlUtil
    {
        private IClientConfiguration _clientConfiguration;

        public UrlUtilImpl(IClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
        }

        public String GetHomeUrl() { return _clientConfiguration.WebHelpUrl + "/home"; }

        public String GetErrorUrl() { return _clientConfiguration.WebHelpUrl + "/error"; }

        /// <exception cref="InvalidUrlException"></exception>
        public String GetContextUrl(String publicationId, String mainKey, String subKey, String languageCode, int articleIndex)
        {
            return _clientConfiguration.WebHelpUrl + "/context/" + publicationId + "/" + mainKey + "/" + subKey + "/" + languageCode + "/" + articleIndex;
        }

        /// <exception cref="InvalidUrlException"></exception>
        public String GetDocumentationUrl(long id, String languageCode, String srcPublicationId)
        {
            String language = String.IsNullOrEmpty(languageCode) ? "" : "/" + languageCode;
            String publicationId = String.IsNullOrEmpty(srcPublicationId) ? "" : srcPublicationId + "/";
            return _clientConfiguration.WebHelpUrl + "/doc/" + publicationId + id + language;
        }


    }
}
