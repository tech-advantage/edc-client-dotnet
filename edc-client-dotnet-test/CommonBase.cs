using edcClientDotnet.factory.impl;
using edcClientDotnet;
using edcClientDotnet.Injection;
using edcClientDotnet.internalImpl;
using edcClientDotnet.internalImpl.http;
using edcClientDotnet.internalImpl.io;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.internalImpl.util;
using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using edcClientDotnetTest.internalImpl;
using edcClientDotnet.factory.model;

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
            ContextItemFactory? contextItemFactory = new ContextItemFactory();
            DocumentationItemFactory? documentationItemFactory = new DocumentationItemFactory();
            IInformationFactory? informationFactory = new InformationFactory();
            I18NFactory? i18nFactory = new I18NFactory();
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
