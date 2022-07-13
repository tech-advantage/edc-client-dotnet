using edc_client_dotnet.internalImpl.http;
using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.io;
using edc_client_dotnet.model;
using edc_client_dotnet.internalImpl;
using static edc_client_dotnet.model.I18NTranslationService;
using edc_client_dotnet.utils;
using Newtonsoft.Json.Linq;
using edc_client_dotnet.utils;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using Ninject.Activation;
using Ninject;
using System.Collections.Generic;

namespace edc_client_dotnet.internalImpl.io
{
    public class HttpReaderImpl : IEdcReader
    {
        private static readonly String MULTI_DOC_FILE = "/multi-doc.json";
        private static readonly String CONTEXT_FILE = "context.json";
        private static readonly String INFO_FILE = "info.json";

        private static readonly String POPOVER_I18N_PATH = "i18n/popover/";
        private static readonly String I18N_FILE_EXTENSION = ".json";
        
        private HttpClientDotnet _httpClientDotnet;
        private IClientConfigurationService _clientConfiguration;
        private IKeyUtil keyUtil;
        private ContextItemService _contextItemService;
        private DocumentationItemService _documentationItemService;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Inject]
        public HttpReaderImpl(
            HttpClientDotnet httpClientDotnet, 
            IClientConfigurationService clientConfigurationService,
            ContextItemService contextItemService,
            DocumentationItemService documentationItemService
        )
        {
            _httpClientDotnet = httpClientDotnet;
            _clientConfiguration = clientConfigurationService;

            _contextItemService = contextItemService;
            _documentationItemService = documentationItemService;
            
           
        }



        public Dictionary<String, IContextItemService> ReadContext()
        {
            Dictionary<String, IContextItemService> contexts = new Dictionary<String, IContextItemService>();
            foreach (String publicationId in ReadPublicationIds())
            {
                
                
                
                foreach (KeyValuePair<String, IContextItemService> test in ReadContext(publicationId)) {
                    Console.WriteLine("Check context " + test.Value);
                    contexts.Add(test.Key, test.Value);
                }
                //contexts.Add();
            }
            Console.WriteLine("Check context " + contexts);
            return contexts;
        }

        public Dictionary<String, IInformationService> ReadInfo()
        {
            Dictionary<String, IInformationService> information = new Dictionary<String, IInformationService>();
            
            foreach (String publicationId in ReadPublicationIds())
            {
                IInformationService info = ReadInfoFile(publicationId);
                information.Add(publicationId, info);
            }
            return information;
        }

        public IInformationService ReadInfoFile(String publicationId)
        {
            String infoFileUrl = _clientConfiguration.GetDocumentationUrl() + "/" + publicationId + "/" + INFO_FILE;
            HashSet<String> defaultLanguagesEnum = new HashSet<String>();
            defaultLanguagesEnum.Add(ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE));
            String infoContent;
            IInformationService infoService = new InformationService();

            try
            {
                infoContent = _httpClientDotnet.GetResponseFromURI(infoFileUrl);
                JObject jsonContent = JObject.Parse(infoContent);

                String defaultLangCode = jsonContent["defaultLanguage"].Value<String>() != null ? jsonContent["defaultLanguage"].Value<String>().ToString() : ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE);
                infoService.SetDefaultLanguage(defaultLangCode);
                logger.Debug("Setting default Language from info.json : {}", defaultLangCode);

                HashSet<String> languages = new HashSet<String>();

                IList<JToken> results = jsonContent["languages"].Children().ToList();

                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    languages.Add(result.ToString());
                }

                infoService.SetLanguages(languages);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (infoService.GetLanguages() == null)
                    infoService.SetDefaultLanguage(ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE));

                if (infoService.GetLanguages() == null)
                    infoService.SetLanguages(defaultLanguagesEnum);

                logger.Debug("Created information from info.json : {}", infoService);
            }

            return infoService;
        }

        public HashSet<String> ReadPublicationIds()
        {
            HashSet<String> publicationIds = new HashSet<String>();
            String multiDocUrl = _clientConfiguration.GetDocumentationUrl() + MULTI_DOC_FILE;
            String content = null;
            try
            {
                content = _httpClientDotnet.GetResponseFromURI(multiDocUrl);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message);
            }

            JArray jsonObject = JArray.Parse(content);

            foreach (JObject value in jsonObject.Children<JObject>())
            {
                publicationIds.Add(value["pluginId"].ToString());
            }
            logger.Info("Publication id");
            //Console.WriteLine("Publication id dans la methode = " + publicationIds);
            return publicationIds;
        }

        private Dictionary<String, IContextItemService> ReadContext(String publicationId)
        {
            String urlContext = _clientConfiguration.GetDocumentationUrl() + "/" + publicationId + "/" + CONTEXT_FILE;
            logger.Info("Context url: {}", urlContext);
            Dictionary<String, IContextItemService> contexts = new Dictionary<String, IContextItemService>();

            try {
                String content = _httpClientDotnet.GetResponseFromURI(urlContext);
                // Decode Json
                JObject jsonContent = JObject.Parse(content);

                foreach (var contentVal in jsonContent){

                    ParseContext(contexts, publicationId, contentVal.Key, contentVal.Value);
                }
            }
            catch(Exception e)
            {
                logger.Warn("No context found, the product was not published", e);
            }
            //Console.WriteLine("Le fucking context " + contexts.Count());
            return contexts;
        }

        private void ParseContext(Dictionary<String, IContextItemService> contexts, String publicationId, String mainKey, JToken jsonElement)
        {
            foreach(var contentVal in jsonElement.ToObject<JObject>())
            {
                ParseContext(contexts, publicationId, mainKey, contentVal.Key, contentVal.Value);
            }
        }

        private void ParseContext(Dictionary<String, IContextItemService> contexts, String publicationId, String mainKey, String subKey, JToken jsonElement)
        {
            //Console.WriteLine("Le JsonElement dans parsecontext subkey = " + jsonElement.ToString());
            foreach (var contentVal in jsonElement.ToObject<JObject>())
            {
               // Console.WriteLine(contentVal.Key + "; " + contentVal.Value + ";");
               // Console.WriteLine("Le JsonElement e.getKey = " + contentVal.Key + "; e.getValue = " + contentVal.Value + ";");
                CreateContext(contexts, publicationId, mainKey, subKey, contentVal.Key, contentVal.Value);
            }
        }
 
        public void CreateContext(Dictionary<String, IContextItemService> contexts, String publicationId, String mainKey, String subKey, String languageCode, JToken jsonElement)
        {
            logger.Debug("Decode for language code: {}", languageCode);
             
            String description = jsonElement.ToObject<JObject>()["description"].Value<String>().ToString();
            Console.WriteLine(" description");
            _contextItemService.SetLabel(getLabel(jsonElement.ToObject<JObject>()));
            _contextItemService.SetLanguageCode(languageCode);
            _contextItemService.SetUrl(getUrl(jsonElement.ToObject<JObject>()));
            _contextItemService.SetPublicationId(publicationId);
            _contextItemService.SetDescription(description);
            _contextItemService.SetMainKey(mainKey);
           // Console.WriteLine(_contextItemService.GetLanguageCode() + " _contextItemService.ToString()");
            CreateArticles(_contextItemService, jsonElement["articles"].Value<JArray>(), languageCode);
            CreateLinks(_contextItemService, jsonElement["links"].Value<JArray>(), languageCode);
            contexts.Add(keyUtil.getKey(mainKey, subKey, languageCode), _contextItemService);
            //Console.WriteLine("Le contexte dans create context = " + contexts);

        }

        private void CreateArticles(IDocumentationItemService documentationItemService, JArray articles, String languageCode)
        {
            foreach(JObject articleJson in articles)
            {
                IDocumentationItemService article = new DocumentationItemService();
                article.SetDocumentationItemType(DocumentationItemType.ARTICLE);
                article.SetLanguageCode(languageCode);
                article.SetId(getId(articleJson));
                article.SetLabel(getLabel(articleJson));
                article.SetUrl(getUrl(articleJson));
                documentationItemService.AddArticle(article);
            }
        }

        private void CreateLinks(IDocumentationItemService documentationItemService, JArray links, String languageCode)
        {
            foreach (JObject linkJson in links)
            {
                IDocumentationItemService link = new DocumentationItemService();
                link.SetDocumentationItemType(GetTypeFromDoc(linkJson));
                link.SetLanguageCode(languageCode);
                link.SetId(getId(linkJson));
                link.SetLabel(getLabel(linkJson));
                link.SetUrl(getUrl(linkJson));
                documentationItemService.AddLink(link);
            }
        }

        private String getLabel(JObject jsonObject) { 
            //Console.WriteLine(jsonObject["label"].Value<String>().ToString());
            return jsonObject["label"].Value<String>().ToString(); 
        }

        private String getUrl(JObject jsonObject) { return jsonObject["url"].Value<String>().ToString(); }

        private long getId(JObject jsonObject) { return jsonObject["id"].Value<String>().LongCount(); }
        private DocumentationItemType GetTypeFromDoc(JObject jsonObject) { return jsonObject["type"].Value<DocumentationItemType>(); }

}
}
