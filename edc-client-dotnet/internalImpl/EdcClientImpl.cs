using edc_client_dotnet.model;
using edc_client_dotnet.utils;

namespace edc_client_dotnet.internalImpl
{
    public class EdcClientImpl : IEdcClient
    {
        private readonly IClientConfiguration _clientConfiguration;
        private readonly IDocumentationManager _documentationManager;
        private readonly ITranslationManager _translationManager;
        private readonly IInformationManager _informationManager;
        private readonly IUrlUtil _urlUtil;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public EdcClientImpl(IClientConfiguration clientConfiguration, IDocumentationManager documentationManager,
            IUrlUtil urlUtil, ITranslationManager translationManager, IInformationManager informationManager)
        {
            _clientConfiguration = clientConfiguration;
            _documentationManager = documentationManager;
            _translationManager = translationManager;
            _informationManager = informationManager;
            _urlUtil = urlUtil;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetContextWebHelpUrl(String mainKey, String subKey, String languageCode)
        {
            _logger.Debug("Get WebHelp Context item with mainKey: {}, subKey: {}, languageCode:{}", mainKey, subKey, languageCode);
            return GetContextWebHelpUrl(mainKey, subKey, 0, languageCode);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetContextWebHelpUrl(String mainKey, String subKey, int rank, String languageCode)
        {
            _logger.Debug("Get WebHelp Context item with mainKey: {}, subKey: {}, languageCode:{}", mainKey, subKey, languageCode);
            String url;
            IContextItem context = _documentationManager.GetContext(mainKey, subKey, languageCode, _translationManager.GetDefaultPublicationLanguages());

            if (context != null && (context.ArticleSize() > 0 || context.LinkSize() > 0))
            {
                url = _urlUtil.GetContextUrl(context.GetPublicationId(), mainKey, subKey, languageCode, rank);
            } else
            {
                url = _urlUtil.GetHomeUrl();
            }
            _logger.Debug("Get WebHelp url: {}", url);
            return url;
        }

        /// <exception cref="InvalidUrlException"></exception>
        public String GetDocumentationWebHelpUrl(long id, String languageCode, String srcPublicationId)
        {
            String url;
            if(id != null)
            {
                url = _urlUtil.GetDocumentationUrl(id, languageCode, srcPublicationId);
            } else
            {
                url = _urlUtil.GetHomeUrl();
            }
            return url;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public IContextItem GetContextItem(String mainKey, String subKey, String languageCode)
        {
            _logger.Debug("Get WebHelp Context item with mainKey: {}, subKey: {}, languageCode:{}", mainKey, subKey, languageCode);
            LoadContext();
            return _documentationManager.GetContext(mainKey, subKey, languageCode, _translationManager.GetDefaultPublicationLanguages());
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetLabel(String labelKey, String languageCode, String publicationId)
        {
            _logger.Debug("Getting label for key {}, language code {} and publication id {}", labelKey, languageCode, publicationId);
            LoadContext();
            return _translationManager.GetLabel(labelKey, languageCode, publicationId);
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetError(String errorKey, String languageCode, String publicationId)
        {
            _logger.Debug("Getting error for key {}, language code {} and publication id {}", errorKey, languageCode, publicationId);
            LoadContext();
            return _translationManager.GetError(errorKey, languageCode, publicationId);
        }
        public void SetServerUrl(String serverUrl)
        {
            _logger.Debug("New server url: {}", serverUrl);
            _clientConfiguration.SetServerUrl(serverUrl);
        }

        /// <exception cref="InvalidUrlException"></exception>
        public void SetWebHelpContextUrl(String webHelpContextUrl)
        {
            _logger.Debug("New WebHelp Context: {}", webHelpContextUrl);
            _clientConfiguration.SetWebHelpContext(webHelpContextUrl);
        }

        /// <exception cref="InvalidUrlException"></exception>
        public void SetDocumentationContextUrl(String documentationContextUrl)
        {
            _logger.Debug("New Documentation Context: {}", documentationContextUrl);
            _clientConfiguration.SetDocumentationContext(documentationContextUrl);
        }

        public void ForceReload()
        {
            _logger.Debug("Force reload");
            _informationManager.ForceReload();
            _translationManager.ForceReload();
            _documentationManager.ForceReload();
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public void LoadContext()
        {
            _logger.Debug("Loading of the configuration");
            _informationManager.LoadInformation();
            _translationManager.LoadTranslations(_informationManager.GetPublicationInformation());
            
            _documentationManager.LoadContext();
        }
    }
}
