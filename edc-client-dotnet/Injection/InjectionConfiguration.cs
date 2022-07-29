using edc_client_dotnet.internalImpl;
using edc_client_dotnet.internalImpl.io;
using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.internalImpl.util;
using edc_client_dotnet.io;
using edc_client_dotnet.model;
using edc_client_dotnet.utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet.Injection
{
    public class InjectionConfiguration
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEdcClient, EdcClientImpl>();
            services.AddSingleton<IDocumentationManager, DocumentationManagerImpl>();
            services.AddSingleton<ITranslationManager, TranslationManagerImpl>();
            services.AddSingleton<IInformationManager, InformationManagerImpl>();

            services.AddSingleton<IKeyUtil, KeyUtilImpl>();
            services.AddScoped<IUrlUtil, UrlUtilImpl>();
            services.AddScoped<ITranslationUtil, TranslationUtilImpl>();

            services.AddSingleton<IClientConfiguration, ClientConfigurationImpl>();
            services.AddSingleton<IContextItem, ContextItemImpl>();
            services.AddSingleton<IEdcReader, HttpReaderImpl>();

            services.AddSingleton<IDocumentationItem, DocumentationItemImpl>();
            
        }
        
    }
}
