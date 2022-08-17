using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ClientConfigurationImpl : IClientConfiguration
    {
        private String? _serverUrl;
        private String _webHelpContext = "help";
        private String _documentationContext = "doc";

        public String GetServerUrl() {
            return _serverUrl;
        }

        public void SetServerUrl(String serverUrl)
        {
            _serverUrl = serverUrl;
        }

        public String GetWebHelpContext() { return _webHelpContext; }

        public void SetWebHelpContext(String webHelpContext)
        {
            if (webHelpContext == null)
                throw new InvalidUrlException("The WebHelp context is null");
            _webHelpContext = webHelpContext;
        }
        public String GetDocumentationContext()
        {
            return _documentationContext;
        }

        public void SetDocumentationContext(String documentationContext)
        {
            _documentationContext = documentationContext ?? throw new InvalidUrlException("The documentation context is null");
        }

        public String GetWebHelpUrl()
        {
            if (String.IsNullOrEmpty(_serverUrl))
                throw new InvalidUrlException("The server url is not defined");

            return _serverUrl.EndsWith("/") ? _serverUrl + _webHelpContext : _serverUrl + "/" + _webHelpContext;
        }

        public String GetDocumentationUrl()
        {
            if (String.IsNullOrEmpty(_serverUrl))
                throw new InvalidUrlException("The server url is not defined");
            return _serverUrl.EndsWith("/") ? _serverUrl + _documentationContext : _serverUrl + "/" + _documentationContext;
        }
    }
}
