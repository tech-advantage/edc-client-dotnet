using edc_client_dotnet.internalImpl.http;
using edc_client_dotnet.internalImpl.io;
using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            
            HttpClientDotnet client = new HttpClientDotnet();
            ClientConfigurationService clientConfiguration = new ClientConfigurationService();
            ContextItemService contextItemService = new ContextItemService();
            DocumentationItemService documentationItemService = new DocumentationItemService();
            clientConfiguration.SetServerUrl("http://localhost:60000");
            logger.Info("Publication id");
            HttpReaderImpl httpReader = new HttpReaderImpl(client, clientConfiguration, contextItemService, documentationItemService);
            httpReader.ReadPublicationIds();
            httpReader.ReadInfoFile("webmailmain");
            httpReader.ReadInfo();
            //httpReader.ReadContext("webmailmain");
            httpReader.ReadContext();
        }
    }
}