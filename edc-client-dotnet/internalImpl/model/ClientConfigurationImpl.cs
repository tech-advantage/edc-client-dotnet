using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ClientConfigurationImpl : IClientConfiguration
    {
        private String _serverUrl;
        private String _webHelpContext = "help";
        private String _documentationContext = "doc";

        public String ServerUrl
        {
            get => _serverUrl;
            set => _serverUrl = value;
        }

        public String WebHelpContext
        { 
            get => _webHelpContext;
            set
            {
                if (value == null)
                    throw new InvalidUrlException("The WebHelp context is null");
                _webHelpContext = value;
            }
        }

        public String DocumentationContext
        {
            get => _documentationContext;
            set
            {
                _documentationContext = value ?? throw new InvalidUrlException("The documentation context is null");
            }
        }

        public String WebHelpUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_serverUrl))
                    throw new InvalidUrlException("The server url is not defined");

                return _serverUrl.EndsWith("/") ? _serverUrl + _webHelpContext : _serverUrl + "/" + _webHelpContext;
            }
            
        }

        public String DocumentationUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_serverUrl))
                    throw new InvalidUrlException("The server url is not defined");
                return _serverUrl.EndsWith("/") ? _serverUrl + _documentationContext : _serverUrl + "/" + _documentationContext;
            }
        }
    }
}
