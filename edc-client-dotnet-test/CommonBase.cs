using edcClientDotnet;
using edcClientDotnet.factory;
using edcClientDotnet.Injection;
using edcClientDotnet.Injection.factory;
using edcClientDotnet.internalImpl;
using edcClientDotnet.internalImpl.http;
using edcClientDotnet.internalImpl.io;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.internalImpl.util;
using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using Microsoft.Extensions.DependencyInjection;

namespace edcClientDotnetTest
{
    public abstract class CommonBase
    {
        private IEdcReader? _edcReader;

        protected HttpClientDotnet CreateHttpClient() { return new HttpClientDotnet(); }

        protected IClientConfiguration CreateClientConfig() { return new ClientConfigurationImpl(); }

        protected IClientConfiguration CreateClientConfiguration()
        {
            IClientConfiguration clientConfiguration = new ClientConfigurationImpl();
            clientConfiguration.SetServerUrl(Constants.SERVER_URL);
            return clientConfiguration;
        }

        protected IKeyUtil CreateKeyBuilder() { return new KeyUtilImpl(); }

        protected IEdcReader CreateEdcReader()
        {
            if (_edcReader != null)
            {
                return _edcReader;
            }
            HttpClientDotnet httpClient = CreateHttpClient();
            IClientConfiguration clientConfiguration = CreateClientConfiguration();
            IKeyUtil keyUtil = CreateKeyBuilder();
            Startup.ConfigureServices();
            ContextItemFactory? contextItemFactory = Startup.serviceProvider.GetService<ContextItemFactory>();
            DocumentationItemFactory? documentationItemFactory = Startup.serviceProvider.GetService<DocumentationItemFactory>();
            InformationFactory? informationFactory = Startup.serviceProvider.GetService<InformationFactory>();
            I18NFactory? i18nFactory = Startup.serviceProvider.GetService<I18NFactory>();
            _edcReader = new HttpReaderImpl(httpClient, clientConfiguration, keyUtil, contextItemFactory, documentationItemFactory, informationFactory, i18nFactory);

            return _edcReader;
        }

        protected IUrlUtil CreateUrlBuilder() { return new UrlUtilImpl(CreateClientConfiguration()); }

        protected ITranslationUtil CreateTranslationUtil() { return new TranslationUtilImpl(); }

        protected IDocumentationManager CreateDocumentationManager() { return new DocumentationManagerImpl(CreateEdcReader(), CreateKeyBuilder()); }

        protected IInformationManager CreateInformationManager(){ return new InformationManagerImpl(CreateEdcReader()); }

        protected ITranslationManager CreateTranslationManager() { return new TranslationManagerImpl(CreateEdcReader(), CreateTranslationUtil()); }

        protected IEdcClient CreateEdcClient() { return new EdcClientImpl(CreateClientConfiguration(), CreateDocumentationManager(), CreateUrlBuilder(), CreateTranslationManager(), CreateInformationManager()); }
    }
}
