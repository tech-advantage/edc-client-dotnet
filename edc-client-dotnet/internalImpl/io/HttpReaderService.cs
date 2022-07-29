using edc_client_dotnet.internalImpl.http;
using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.io;
using edc_client_dotnet.model;
using static edc_client_dotnet.model.I18NTranslationService;
using edc_client_dotnet.utils;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Ninject;
using edc_client_dotnet.factory;

namespace edc_client_dotnet.internalImpl.io
{
    public class HttpReaderService : IEdcReader
    {
        private static readonly String MULTI_DOC_FILE = "/multi-doc.json";
        private static readonly String CONTEXT_FILE = "context.json";
        private static readonly String INFO_FILE = "info.json";

        private static readonly String POPOVER_I18N_PATH = "i18n/popover/";
        private static readonly String I18N_FILE_EXTENSION = ".json";
        
        private HttpClientDotnet _httpClientDotnet;
        private IClientConfigurationService _clientConfiguration;
        private IKeyUtil keyUtil;
        private ContextItemFactory _contextItemFactory;
        private DocumentationItemFactory _documentationItemFactory;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
   

        [Inject]
        public HttpReaderService(
            HttpClientDotnet httpClientDotnet,
            IClientConfigurationService clientConfigurationService,
            ContextItemFactory contextItemFactory,
            DocumentationItemFactory documentationItemFactory
        )
        {
            _httpClientDotnet = httpClientDotnet;
            _clientConfiguration = clientConfigurationService;
            _contextItemFactory = contextItemFactory;
            _documentationItemFactory = documentationItemFactory;
        }

        public Dictionary<String, IContextItemService> ReadContext()
        {
            
            Dictionary<String, IContextItemService> contexts = new Dictionary<String, IContextItemService>();
           
            foreach (var publicationId in ReadPublicationIds())
            {
                Console.WriteLine("Publication id = " + publicationId);
                foreach (KeyValuePair<String, IContextItemService> value in ReadContext(publicationId)) {
                    
                    contexts.Add(value.Key, value.Value);
                }
            }

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

        private HashSet<String> ReadPublicationIds()
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

                foreach (var value in jsonContent){
                    ParseContext(contexts, publicationId, value.Key, value.Value);
                }
            }
            catch(Exception e)
            {
                logger.Warn("No context found, the product was not published", e);
            }

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
            foreach (var value in jsonElement.ToObject<JObject>())
            {
                CreateContext(contexts, publicationId, mainKey, subKey, value.Key, value.Value);
            }
        }
 
        private void CreateContext(Dictionary<String, IContextItemService> contexts, String publicationId, String mainKey, String subKey, String languageCode, JToken? jsonElement)
        {
            logger.Debug("Decode for language code: {}", languageCode);
          
            String description = jsonElement["description"].Value<String>();
            ContextItemService contextItem = _contextItemFactory.Create();
            
            contextItem.SetLabel(getLabel(jsonElement.ToObject<JObject>()));
            contextItem.SetLanguageCode(languageCode);
            contextItem.SetUrl(getUrl(jsonElement.ToObject<JObject>()));
            contextItem.SetPublicationId(publicationId);
            contextItem.SetDescription(description);
            contextItem.SetMainKey(mainKey);

            CreateArticles(contextItem, jsonElement["articles"].Value<JArray>(), languageCode);
            CreateLinks(contextItem, jsonElement["links"].Value<JArray>(), languageCode);
        }

        private void CreateArticles(IDocumentationItemService documentationItem, JArray articles, String languageCode)
        {
            
            foreach(JObject articleJson in articles)
            {
                DocumentationItemService article = _documentationItemFactory.Create();
                article.SetDocumentationItemType(DocumentationItemType.ARTICLE);
                article.SetLanguageCode(languageCode);
                article.SetId(getId(articleJson));
                article.SetLabel(getLabel(articleJson));
                article.SetUrl(getUrl(articleJson));
                documentationItem.AddArticle(article);
            }
        }

        private void CreateLinks(IDocumentationItemService documentationItem, JArray links, String languageCode)
        {
            foreach (JObject linkJson in links)
            {
                DocumentationItemType linksType = linkJson["type"].Value<String>() == "CHAPTER" ? DocumentationItemType.CHAPTER : linkJson["type"].Value<String>() == "DOCUMENT" ? DocumentationItemType.DOCUMENT : DocumentationItemType.UNKNOWN;
                DocumentationItemService link = _documentationItemFactory.Create();
                link.SetDocumentationItemType(linksType);
                link.SetLanguageCode(languageCode);
                link.SetId(getId(linkJson));
                link.SetLabel(getLabel(linkJson));
                link.SetUrl(getUrl(linkJson));
                documentationItem.AddLink(link);
            }
        }

        public Dictionary<String, String> ReadI18nContent(String languageCode, String key)
        {
            II18NContentService i18nContentService = new I18NContentService();
            String content;
            Dictionary<String, String> i18nLabels = new Dictionary<string, string>();
            String relativePath = POPOVER_I18N_PATH + "en" + I18N_FILE_EXTENSION;
            String i18nUrl = _clientConfiguration.GetDocumentationUrl() + "/" + relativePath;
            logger.Debug("Reading labels for lang {}, url {}, labelUrl {}", languageCode, relativePath, relativePath);

            try
            {
                content = _httpClientDotnet.GetResponseFromURI(_clientConfiguration.GetDocumentationUrl() + "/" + relativePath);

                JObject jsoni18nContent = JObject.Parse(content);

                Dictionary<string, string> dictObj = jsoni18nContent[key].ToObject<Dictionary<string, string>>();

                foreach (var value in dictObj)
                {
                    i18nLabels.Add(value.Key, value.Value);
                }

                if(key == ParseEnumDescription.GetDescription(I18N_LABELS_ROOT))
                {
                    i18nContentService.SetI18nLabel(i18nLabels);
                } 
                else
                {
                    i18nContentService.SetI18nError(i18nLabels);
                }
            }
            catch (Exception e)
            {
                logger.Error("Could not read the labels for the lang {}, err {}", languageCode, e);
            }

            return key == ParseEnumDescription.GetDescription(I18N_LABELS_ROOT) ? i18nContentService.GetLabel() : i18nContentService.GetError();
        }

        private String getLabel(JObject? jsonObject) { 
            if(jsonObject is not null)
                return jsonObject["label"].Value<String>().ToString();

            return null;
        }

        private Dictionary<String, String> ReadLabelsForLang(String languageCode, String key)
        {
            return null;
        }

        private String getUrl(JObject? jsonObject) { return jsonObject["url"] .Value<String>().ToString(); }

        private long getId(JObject jsonObject) { return jsonObject["id"].Value<String>().LongCount(); }

    }
}
