using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class ClientConfigurationService : IClientConfigurationService
    {
        private IClientConfigurationService _clientConfiguration;
        private String serverUrl = "";
        private String webHelpContext = "/help";
        private String documentationContext = "/doc";

        public string GetDocumentationContext()
        {
             return documentationContext;
        }

        public string GetDocumentationUrl()
        {
            if (String.IsNullOrEmpty(serverUrl))
                throw new InvalidUrlException("The server url is not defined");

            return serverUrl + documentationContext;
        }

        public string GetServerUrl()
        {
            return serverUrl;
        }

        public string GetWebHelpContext()
        {
            return webHelpContext;
        }

        public string GetWebHelpUrl()
        {
            if (String.IsNullOrEmpty(serverUrl))
                throw new InvalidUrlException("The server url is not defined");

            return serverUrl + webHelpContext;
        }

        public void SetDocumentationContext(string documentationContext)
        {
            if (documentationContext == null)
                throw new InvalidUrlException("The documentation context is null");
            this.documentationContext = documentationContext;
        }

        public void SetServerUrl(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }

        public void SetWebHelpContext(string webHelpContext)
        {
            if (webHelpContext == null)
                throw new InvalidUrlException("The WebHelp context is null");
            this.webHelpContext = webHelpContext;
        }
    }
}
