using edc_client_dotnet.model;
using System.Collections.ObjectModel;

namespace edc_client_dotnet.internalImpl.model
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

        public DocumentationItemImpl() {}

        public DocumentationItemImpl(DocumentationItemType documentationItemType)
        {
            _documentationItemType = documentationItemType;
        }

        public DocumentationItemType GetDocumentationItemType() { return _documentationItemType; }

        public String GetLabel() { return _label; }

        public String GetLanguageCode() { return _languageCode; }

        public String GetPublicationId() { return _publicationId; }

        public String GetUrl() { return _url; }

        public void SetDocumentationItemType(DocumentationItemType documentationItemType) { _documentationItemType = documentationItemType; }

        public void SetLabel(String label) { _label = label; }

        public void SetLanguageCode(String languageCode) { _languageCode = languageCode; }

        public void SetPublicationId(String publicationId) { _publicationId = publicationId; }

        public void SetUrl(String? url)
        {
            if (url is not null)
                _url = url;
        }

        public void AddArticle(IDocumentationItem article)
        {
            if (article.GetDocumentationItemType() == DocumentationItemType.ARTICLE)
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
            if (link.GetDocumentationItemType() != DocumentationItemType.ARTICLE)
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
