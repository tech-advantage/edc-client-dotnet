using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using NLog;

namespace edcClientDotnet.internalImpl
{
    public class DocumentationManagerImpl : IDocumentationManager
    {
        private readonly IEdcReader _reader;
        private readonly IKeyUtil _keyUtil;
        private Dictionary<String, IContextItem>? _contexts;
        private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        public DocumentationManagerImpl(IEdcReader reader, IKeyUtil keyBuilder)
        {
            _reader = reader;
            _keyUtil = keyBuilder;
        }

        public IContextItem? GetContext(String? mainKey, String? subKey, String? languageCode, IReadOnlyDictionary<String, String> defaultLanguages)
        {
            
            _logger.Debug("Get Context item with mainkey: {}, subKey: {}, languageCode:{}", mainKey, subKey, languageCode);
            LoadContext();

            IContextItem? contextItem = _contexts.GetValueOrDefault(_keyUtil.GetKey(mainKey, subKey, languageCode));
            
            if (contextItem == null && !String.IsNullOrEmpty(languageCode))
            {
                _logger.Debug("Context item was null, getting from defaultLanguages:{}", defaultLanguages);
                contextItem = FindOrDefaultContextItem(mainKey, subKey, defaultLanguages);
            }

            return contextItem;
        }

        public void ForceReload()
        {
            _logger.Debug("Force reload on next call");
            _contexts = null;
        }

        public void LoadContext()
        {
            if (_contexts == null)
            {
                _logger.Debug("No contexts defined, read it");
                _contexts = _reader.GetContext();
            }
        }

        private IContextItem? FindOrDefaultContextItem(String mainKey, String subKey, IReadOnlyDictionary<String, String> defaultLangCodes)
        {
            IContextItem? defaultContext = null;
            HashSet<IContextItem> presentItems = _contexts.Where(e => _keyUtil.ContainsKey(e.Key, mainKey, subKey)).Select(row => row.Value).ToHashSet();
            
            if (presentItems.Any())
            {
                String exportId = String.IsNullOrEmpty(presentItems.Select(s => s.PublicationId).First()) ? "" : presentItems.Select(s => s.PublicationId).First();
                String defaultLang = defaultLangCodes.GetValueOrDefault(exportId);
                defaultContext = _contexts.GetValueOrDefault(_keyUtil.GetKey(mainKey, subKey, defaultLang));
            }
            return defaultContext;
        }

    }
}
