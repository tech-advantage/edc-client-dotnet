using edc_client_dotnet;
using edc_client_dotnet.factory;
using edc_client_dotnet.Injection;
using edc_client_dotnet.Injection.factory;
using edc_client_dotnet.internalImpl;
using edc_client_dotnet.internalImpl.http;
using edc_client_dotnet.internalImpl.io;
using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.internalImpl.util;
using edc_client_dotnet.io;
using edc_client_dotnet.model;
using edc_client_dotnet.utils;
using Microsoft.Extensions.DependencyInjection;

namespace edc_client_dotnet_test
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
