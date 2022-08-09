using edc_client_dotnet.factory;
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

namespace edc_client_dotnet.Injection
{
    public static class Startup
    {
        public static IServiceProvider? serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<HttpClient>();
            services.AddSingleton<HttpClientDotnet>().AddSingleton<HttpClient, HttpClientDotnet>(s => s.GetService<HttpClientDotnet>());
            services.AddSingleton<IEdcClient, EdcClientImpl>();
            services.AddSingleton<IDocumentationManager, DocumentationManagerImpl>();
            services.AddSingleton<ITranslationManager, TranslationManagerImpl>();
            services.AddSingleton<IInformationManager, InformationManagerImpl>();
            services.AddSingleton<IKeyUtil, KeyUtilImpl>();
            services.AddSingleton<IClientConfiguration, ClientConfigurationImpl>();

            services.AddTransient<IUrlUtil, UrlUtilImpl>();
            services.AddTransient<ITranslationUtil, TranslationUtilImpl>();
            services.AddTransient<IEdcReader, HttpReaderImpl>();

            // Factory
            services.AddTransient<ContextItemFactory>();
            services.AddTransient<ContextItemImpl>().AddTransient<IContextItem, ContextItemImpl>(s => s.GetService<ContextItemImpl>());

            services.AddTransient<DocumentationItemFactory>();
            services.AddTransient<DocumentationItemImpl>().AddTransient<IDocumentationItem, DocumentationItemImpl>(s => s.GetService<DocumentationItemImpl>());

            services.AddTransient<I18NFactory>();
            services.AddTransient<I18NContentImpl>().AddTransient<II18NContent, I18NContentImpl>(s => s.GetService<I18NContentImpl>());

            services.AddTransient<InformationFactory>();
            services.AddTransient<InformationImpl>().AddTransient<IInformation, InformationImpl>(s => s.GetService<InformationImpl>());

            serviceProvider = services.BuildServiceProvider();
        }

    }
}
