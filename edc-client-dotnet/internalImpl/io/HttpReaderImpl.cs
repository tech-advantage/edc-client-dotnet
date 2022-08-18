using edcClientDotnet.factory.model;
using edcClientDotnet.internalImpl.http;
using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using Newtonsoft.Json.Linq;
using NLog;
using static edcClientDotnet.model.I18NTranslation;

namespace edcClientDotnet.internalImpl.io
{
    public class HttpReaderImpl : IEdcReader
    {
        private static readonly String MultiDocFile = "multi-doc.json";
        private static readonly String ContextFile = "context.json";
        private static readonly String InfoFile = "info.json";
        private static readonly String PopoverI18NPath = "i18n/popover/";
        private static readonly String I18NFileExtension = ".json";
        
        private readonly HttpClientDotnet _httpClientDotnet;
        private IClientConfiguration _clientConfiguration { get; }
        private readonly IKeyUtil _keyUtil;
        private readonly IContextItemFactory _contextItemFactory;
        private IDocumentationItemFactory _documentationItemFactory;
        private IInformationFactory _informationFactory;
        private II18NFactory _i18NFactory;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public HttpReaderImpl(HttpClientDotnet httpClientDotnet, IClientConfiguration clientConfigurationService, IKeyUtil keyUtil, IContextItemFactory contextItemFactory,
                                IDocumentationItemFactory documentationItemFactory, IInformationFactory informationFactory, II18NFactory i18NFactory)
        {
            _httpClientDotnet = httpClientDotnet;
            _clientConfiguration = clientConfigurationService;
            _keyUtil = keyUtil;
            _contextItemFactory = contextItemFactory;
            _documentationItemFactory = documentationItemFactory;
            _informationFactory = informationFactory;
            _i18NFactory = i18NFactory;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public Dictionary<String, IContextItem> GetContext()
        {
            Dictionary<String, IContextItem> contexts = new Dictionary<String, IContextItem>();
           
            foreach (var publicationId in ReadPublicationIds())
            {
                contexts = ReadContext(publicationId);
            }
            return contexts;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public Dictionary<String, IInformation> GetInformations()
        {
            Dictionary<String, IInformation> information = new Dictionary<String, IInformation>();
            foreach (String publicationId in ReadPublicationIds())
            {
                IInformation info = ReadInfoFile(publicationId);
                if(info is not null)
                {
                    information.Add(publicationId, info);
                }
            }
            return information;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        private IInformation ReadInfoFile(String publicationId)
        {
            String infoFileUrl = _clientConfiguration.GetDocumentationUrl().EndsWith("/") 
                ? _clientConfiguration.GetDocumentationUrl() + publicationId + "/" + InfoFile
                : _clientConfiguration.GetDocumentationUrl() + "/" + publicationId + "/" + InfoFile;
           
            HashSet<String> defaultLanguageCode = new HashSet<String>
            {
                ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE)
            };

            IInformation information = _informationFactory.Create();

            try
            {
                String infoContent = _httpClientDotnet.GetAsyncResponse(infoFileUrl);
                JObject jsonContent = JObject.Parse(infoContent);
                _logger.Debug("Fetched content from info.json file {}", jsonContent);

                if (jsonContent is JObject)
                {
                    HashSet<String> languages = new HashSet<String>();
                    String defaultLangCode = jsonContent["defaultLanguage"]?.Value<String>() != null ? jsonContent["defaultLanguage"].Value<String>() : ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE);
                    information.SetDefaultLanguage(defaultLangCode);
                    _logger.Debug("Setting default Language from info.json : {}", defaultLangCode);
                    IList<JToken> presentLanguage = jsonContent["languages"].Children().ToList();

                    if (presentLanguage != null)
                    {
                        foreach (JToken lang in presentLanguage)
                        {
                            languages.Add(lang.ToString());
                        }
                    }
                    _logger.Debug("Setting languages from info.json : {}", languages);
                    information.SetLanguages(languages);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                _logger.Error("Could not initialize info from info.json for publication id : {}", publicationId, e);
            }
            finally
            {
                if (String.IsNullOrEmpty(information.GetDefaultLanguage()))
                    information.SetDefaultLanguage(ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE));
                if (information.GetLanguages() == null || information.GetLanguages().Any())
                    information.SetLanguages(defaultLanguageCode);
                _logger.Debug("Created information from info.json : {}", information);
            }

            return information;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        private HashSet<String> ReadPublicationIds()
        {
            HashSet<String> publicationIds = new HashSet<String>();
            String multiDocUrl = _clientConfiguration.GetDocumentationUrl().EndsWith("/") 
                ? _clientConfiguration.GetDocumentationUrl() + MultiDocFile 
                : _clientConfiguration.GetDocumentationUrl() + "/" + MultiDocFile;
            _logger.Info("Multidoc url: {}", multiDocUrl);
            String? content = null;
            try
            {
                content = _httpClientDotnet.GetAsyncResponse(multiDocUrl);
            }
            catch (Exception e)
            {
                throw new IOException(e.Message);
            }

            JArray jsonContent = JArray.Parse(content);
            if (jsonContent is JArray)
            {
                foreach (JObject value in jsonContent.Children<JObject>())
                {
                    publicationIds.Add(value["pluginId"].ToString());
                }
            }

            return publicationIds;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        private Dictionary<String, IContextItem> ReadContext(String publicationId)
        {
            String urlContext = _clientConfiguration.GetDocumentationUrl().EndsWith("/") 
                ? _clientConfiguration.GetDocumentationUrl() + publicationId + "/" + ContextFile
                : _clientConfiguration.GetDocumentationUrl() + "/" + publicationId + "/" + ContextFile;
            
            _logger.Info("Context url: {}", urlContext);
            Dictionary<String, IContextItem> contexts = new Dictionary<String, IContextItem>();
            try {
                String content = _httpClientDotnet.GetAsyncResponse(urlContext);
                // Decode Json
                JObject jsonContent = JObject.Parse(content);
                if (jsonContent is JObject)
                {
                    _logger.Debug("json Object: {}", jsonContent);
                    foreach (KeyValuePair<String, JToken?> entry in jsonContent)
                    {
                        ParseContext(contexts, publicationId, entry.Key, entry.Value);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Warn("No context found, the product was not published", e);
            }

            return contexts;
        }

        private void ParseContext(Dictionary<String, IContextItem> contexts, String publicationId, String mainKey, JToken? jsonElement)
        {
            _logger.Debug("Decode for main key: {}", mainKey);
            foreach (KeyValuePair<String, JToken?> entry in jsonElement.ToObject<JObject>())
            {
                ParseContext(contexts, publicationId, mainKey, entry.Key, entry.Value);
            }
        }

        private void ParseContext(Dictionary<String, IContextItem> contexts, String publicationId, String mainKey, String subKey, JToken? jsonElement)
        {
            _logger.Debug("Decode for sub key: {}", subKey);
            foreach (KeyValuePair<String, JToken?> entry in jsonElement.ToObject<JObject>())
            {
                CreateContext(contexts, publicationId, mainKey, subKey, entry.Key, entry.Value);
            }
        }
 
        private void CreateContext(Dictionary<String, IContextItem> contexts, String publicationId, String mainKey, String subKey, String languageCode, JToken? jsonElement)
        {
            _logger.Debug("Decode for language code: {}", languageCode);
            String description = jsonElement["description"].Value<String>();
            IContextItem contextItem = _contextItemFactory.Create();
            contextItem.SetLabel(GetLabel(jsonElement.ToObject<JObject>()));
            contextItem.SetLanguageCode(languageCode);
            contextItem.SetUrl(getUrl(jsonElement.ToObject<JObject>()));
            contextItem.SetPublicationId(publicationId);
            contextItem.SetDescription(description);
            contextItem.SetMainKey(mainKey);

            CreateArticles(contextItem, jsonElement["articles"].Value<JArray>(), languageCode);
            CreateLinks(contextItem, jsonElement["links"].Value<JArray>(), languageCode);
            contexts.Add(_keyUtil.GetKey(mainKey, subKey, languageCode), contextItem);
        }

        private void CreateArticles(IDocumentationItem documentationItem, JArray articles, String languageCode)
        {
            foreach(JObject articleJson in articles)
            {
                _logger.Debug("Article to decode: {}", articleJson);
                IDocumentationItem article = _documentationItemFactory.Create();
                article.SetDocumentationItemType(DocumentationItemType.ARTICLE);
                article.SetLanguageCode(languageCode);
                article.SetId(getId(articleJson));
                article.SetLabel(GetLabel(articleJson));
                article.SetUrl(getUrl(articleJson));
                _logger.Debug("new article: {}, id {}, label {}, url {}, documentationItemType {}, articles = [{}], links = [{}]", article, article.GetId(), article.GetLabel(), article.GetUrl(), article.GetDocumentationItemType(), article.GetArticles(), article.GetLinks());
                documentationItem.AddArticle(article);
            }
        }

        private void CreateLinks(IDocumentationItem documentationItem, JArray links, String languageCode)
        {
            foreach (JObject linkJson in links)
            {
                _logger.Debug("link to decode: {}", linkJson);
                DocumentationItemType linksType = linkJson["type"]?.Value<String>() == "CHAPTER" ? DocumentationItemType.CHAPTER : linkJson["type"]?.Value<String>() == "DOCUMENT" ? DocumentationItemType.DOCUMENT : DocumentationItemType.UNKNOWN;
                IDocumentationItem link = _documentationItemFactory.Create();
                link.SetDocumentationItemType(linksType);
                link.SetLanguageCode(languageCode);
                link.SetId(getId(linkJson));
                link.SetLabel(GetLabel(linkJson));
                link.SetUrl(getUrl(linkJson));
                _logger.Debug("new link: {}", link);
                documentationItem.AddLink(link);
            }
        }

        /// <exception cref="InvalidUrlException"></exception>
        private String GetLabelUrl(String languageCode)
        {
            String relativePath = PopoverI18NPath + languageCode + I18NFileExtension;
            String i18nLabelUrl = _clientConfiguration.GetDocumentationUrl().EndsWith("/") 
                ? _clientConfiguration.GetDocumentationUrl() + relativePath
                : _clientConfiguration.GetDocumentationUrl() + "/" + relativePath;

            _logger.Debug("Reading labels for lang {}, url {}, labelUrl {}", languageCode, relativePath, i18nLabelUrl);
            return i18nLabelUrl;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        private void ReadI18nContent(String languageCode, II18NContent i18NContent)
        {
            String label;
            String labelUrl = GetLabelUrl(languageCode);

            try
            {
                label = _httpClientDotnet.GetAsyncResponse(labelUrl);
                JObject? jsonContent = JObject.Parse(label);
                HashSet<String> labels = new HashSet<String>();

                foreach(KeyValuePair<String, JToken?> i18nLabelKey in jsonContent)
                {
                    labels.Add(i18nLabelKey.Key);
                }

                foreach (String key in labels)
                {
                    Dictionary<String, String> dictObj = jsonContent[key].ToObject<Dictionary<String, String>>();

                    foreach (KeyValuePair<String, String> value in dictObj)
                    {
                        i18NContent.SetMessage(languageCode, key, value.Key, value.Value);
                    }
                }
            }
            catch (Exception e)
            {
               _logger.Error("Could not read the labels for the lang {}, err {}", languageCode, e);
            }
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public II18NContent ReadLabel(HashSet<String> languagesCode)
        {
            II18NContent i18NContent = _i18NFactory.Create();

            if(languagesCode != null)
            {
                foreach(String language in languagesCode)
                {
                    ReadI18nContent(language, i18NContent);
                }
            }
            
            return i18NContent;
        }

        private String GetLabel(JObject? jsonObject) { return jsonObject["label"].Value<String>(); }

        private String getUrl(JObject? jsonObject) { return jsonObject["url"].Value<String>(); }

        private long getId(JObject? jsonObject) { return jsonObject["id"].Value<String>().LongCount(); }
    }
}
