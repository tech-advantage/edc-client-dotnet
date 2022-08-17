using edcClientDotnet.Injection;
using edcClientDotnet.model;
using Microsoft.Extensions.DependencyInjection;

namespace edcClientDotnet
{
    public class EdcClientSingleton : IEdcClient
    {
        private static EdcClientSingleton? _instance = null;
        private IEdcClient _edcClient;

        private EdcClientSingleton() : base() {}

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
        public IContextItem GetContextItem(String mainKey, String subKey, String languageCode)
        {
            return _edcClient.GetContextItem(mainKey, subKey, languageCode);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetLabel(String labelKey, String languageCode, String publicationId)
        {
            return _edcClient.GetLabel(labelKey, languageCode, publicationId);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetError(String errorKey, String languageCode, String publicationId)
        {
            return _edcClient.GetError(errorKey, languageCode, publicationId);
        }

        public void SetServerUrl(String serverUrl){
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
