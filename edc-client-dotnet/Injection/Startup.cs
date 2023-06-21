using edcClientDotnet.factory;
using edcClientDotnet.internalImpl;
using edcClientDotnet.internalImpl.factory;
using edcClientDotnet.internalImpl.http;
using edcClientDotnet.internalImpl.io;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.internalImpl.util;
using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using Microsoft.Extensions.DependencyInjection;

namespace edcClientDotnet.Injection
{
    public static class Startup
    {
        public static IServiceProvider? serviceProvider;
        public static IServiceCollection services = new ServiceCollection();
        public static void ConfigureServices()
        {
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

            services.AddTransient<IContextItemFactory, ContextItemFactory>();
            services.AddTransient<IDocumentationItemFactory, DocumentationItemFactory>();
            services.AddTransient<IInformationFactory, InformationFactory>();
            services.AddTransient<II18NFactory, I18NFactory>();

            serviceProvider = services.BuildServiceProvider();
        }

    }
}
