using edc_client_dotnet.model;
using System.Collections.ObjectModel;

namespace edc_client_dotnet.internalImpl.model
{
    public class DocumentationItemService : ObjectIdService, IDocumentationItemService
    {
        private String? label;
        private String? publicationId;
        private String? url;
        private String? languageCode;
        private DocumentationItemType documentationItemType;
        private List<IDocumentationItemService> articles = new List<IDocumentationItemService>();
        private List<IDocumentationItemService> links = new List<IDocumentationItemService>();
        private DocumentationItemType CONTEXTUAL;

        public DocumentationItemService()
        {
        }

        public DocumentationItemService(DocumentationItemType documentationItemType)
        {
            this.documentationItemType = documentationItemType;
        }

        public void AddArticle(IDocumentationItemService article)
        {
            if (article.GetDocumentationItemType() == DocumentationItemType.ARTICLE)
            {
                articles.Add(article);
            }
        }

        public void AddLink(IDocumentationItemService link)
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

        public void SetUrl(string? url)
        {
            if (url is not null)
                this.url = url;
        }

        ReadOnlyCollection<IDocumentationItemService> IDocumentationItemService.GetArticles()
        {
            return articles.AsReadOnly();
        }

        ReadOnlyCollection<IDocumentationItemService> IDocumentationItemService.GetLinks()
        {
            return links.AsReadOnly();
        }
    }
}
