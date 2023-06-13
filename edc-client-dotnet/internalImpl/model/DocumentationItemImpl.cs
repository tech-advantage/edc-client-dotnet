using edcClientDotnet.model;
using System.Collections.ObjectModel;

namespace edcClientDotnet.internalImpl.model
{
    public class DocumentationItemImpl : ObjectIdImpl, IDocumentationItem
    {
        private String? _label;
        private String? _publicationId;
        private String? _url;
        private String? _languageCode;
        private DocumentationItemType _documentationItemType;
        private List<IDocumentationItem> _articles = new List<IDocumentationItem>();
        private List<IDocumentationItem> _links = new List<IDocumentationItem>();

        public DocumentationItemImpl() { }

        public DocumentationItemImpl(DocumentationItemType documentationItemType)
        {
            _documentationItemType = documentationItemType;
        }

        public DocumentationItemType DocumentationItemType
        {
            get => _documentationItemType;
            set => _documentationItemType = value;
        }

        public String Label
        {
            get => _label;
            set => _label = value;
        }

        public String Url
        {
            get => _url;
            set
            {
                if (value is not null)
                    _url = value;
            }
        }

        public String PublicationId
        {
            get => _publicationId;
            set => _publicationId = value;
        }

        public String LanguageCode
        {
            get => _languageCode;
            set => _languageCode = value;
        }

        public void AddArticle(IDocumentationItem article)
        {
            if (article.DocumentationItemType == DocumentationItemType.ARTICLE)
            {
                _articles.Add(article);
            }
        }

        ReadOnlyCollection<IDocumentationItem> IDocumentationItem.GetArticles()
        {
            return _articles.AsReadOnly();
        }

        public int ArticleSize() { return _articles.Count(); }

        public void AddLink(IDocumentationItem link)
        {
            if (link.DocumentationItemType != DocumentationItemType.ARTICLE)
            {
                _links.Add(link);
            }
        }

        ReadOnlyCollection<IDocumentationItem> IDocumentationItem.GetLinks()
        {
            return _links.AsReadOnly();
        }

        public int LinkSize() { return _links.Count(); }
    }
}
