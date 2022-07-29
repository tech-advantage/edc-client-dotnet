using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
{
    public class DocumentationItemFactory
    {
        public IDocumentationItem Create()
        {
            return new DocumentationItemImpl();
        }
    }
}
