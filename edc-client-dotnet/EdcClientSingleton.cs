using edcClientDotnet.Injection;
using edcClientDotnet.model;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace edcClientDotnet
{
    public class EdcClientSingleton : IEdcClient
    {
        private static EdcClientSingleton? _instance = null;
        private IEdcClient _edcClient;
        private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        private EdcClientSingleton() : base() { }

        public static EdcClientSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EdcClientSingleton();
                _instance.Init();
            }
            return _instance;
        }

        private void Init()
        {
            Startup.ConfigureServices();
            _edcClient = Startup.serviceProvider.GetRequiredService<IEdcClient>();
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetContextWebHelpUrl(String mainKey, String subKey, String languageCode)
        {
            return _edcClient.GetContextWebHelpUrl(mainKey, subKey, languageCode);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetContextWebHelpUrl(String mainKey, String subKey, int rank, String languageCode)
        {
            return _edcClient.GetContextWebHelpUrl(mainKey, subKey, rank, languageCode);
        }

        /// <exception cref="InvalidUrlException"></exception>
        public String GetDocumentationWebHelpUrl(long id, String languageCode, String srcPublicationId)
        {
            return _edcClient.GetDocumentationWebHelpUrl(id, languageCode, srcPublicationId);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public IContextItem GetContextItem(String? mainKey, String? subKey, String? languageCode)
        {
            return _edcClient.GetContextItem(mainKey, subKey, languageCode);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetLabel(String labelKey, String languageCode, String publicationId)
        {
            try
            {
                return _edcClient.GetLabel(labelKey, languageCode, publicationId);
            }
            catch (IOException ex)
            {
                // Handle IOException here
                // You can log the exception, display an error message, or take appropriate action
                // Example:
                _logger.Error("An IOException occurred: " + ex.Message);
            }
            catch (InvalidUrlException ex)
            {
                // Handle InvalidUrlException here
                // Example:
                _logger.Error("An InvalidUrlException occurred: " + ex.Message);
            }

            // Return a default value or handle the exceptional case
            return string.Empty;

        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetError(String errorKey, String languageCode, String publicationId)
        {
            return _edcClient.GetError(errorKey, languageCode, publicationId);
        }

        public void SetServerUrl(String serverUrl)
        {
            _edcClient.SetServerUrl(serverUrl);
        }

        /// <exception cref="InvalidUrlException"></exception>
        public void SetWebHelpContextUrl(String webHelpContextUrl)
        {
            _edcClient.SetWebHelpContextUrl(webHelpContextUrl);
        }

        /// <exception cref="InvalidUrlException"></exception>
        public void SetDocumentationContextUrl(String documentationContextUrl)
        {
            _edcClient.SetDocumentationContextUrl(documentationContextUrl);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public void LoadContext() { _edcClient.LoadContext(); }

        public void ForceReload() { _edcClient.ForceReload(); }
    }
}
