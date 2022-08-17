using edcClientDotnet.factory;
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

namespace edcClientDotnet.Injection
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
