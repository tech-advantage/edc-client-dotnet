using edc_client_dotnet.model;
using System.Collections.ObjectModel;

namespace edc_client_dotnet.internalImpl.model
{
    internal class DocumentationItemImpl : ObjectIdImpl, IDocumentationItem
    {
        private String? label;
        private String? publicationId;
        private String? url;
        private String? languageCode;
        private DocumentationItemType documentationItemType;
        private List<IDocumentationItem> articles = new List<IDocumentationItem>();
        private List<IDocumentationItem> links = new List<IDocumentationItem>();

        public DocumentationItemImpl() { }
        public DocumentationItemImpl(DocumentationItemType documentationItemType)
        {
            this.documentationItemType = documentationItemType;
        }

        public void AddArticle(IDocumentationItem article)
        {
            if (article.GetDocumentationItemType() == DocumentationItemType.ARTICLE)
            {
                articles.Add(article);
            }
        }

        public void AddLink(IDocumentationItem link)
        {
            if (link.GetDocumentationItemType() != DocumentationItemType.ARTICLE)
            {
                links.Add(link);
            }
        }

        public int ArticleSize() { return articles.Count(); }

        public DocumentationItemType GetDocumentationItemType() { return documentationItemType; }

        public string GetLabel() { return label; }

        public string GetLanguageCode() { return languageCode; }

        public string GetPublicationId() { return publicationId; }

        public string GetUrl() { return url; }

        public int LinkSize() { return links.Count(); }

        public void SetDocumentationItemType(DocumentationItemType documentationItemType)
        {
            this.documentationItemType = documentationItemType;
        }

        public void SetLabel(string label)
        {
           this.label = label;
        }

        public void SetLanguageCode(string languageCode)
        {
            this.languageCode = languageCode;
        }

        public void SetPublicationId(string publicationId)
        {
            this.publicationId = publicationId;
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }

        ReadOnlyCollection<IDocumentationItem> IDocumentationItem.GetArticles()
        {
            return articles.AsReadOnly();
        }

        ReadOnlyCollection<IDocumentationItem> IDocumentationItem.GetLinks()
        {
            return links.AsReadOnly();
        }
    }
}
