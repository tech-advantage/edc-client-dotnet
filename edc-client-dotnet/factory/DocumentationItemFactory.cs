using edc_client_dotnet.internalImpl.model;

namespace edc_client_dotnet.factory
{
    public class DocumentationItemFactory
    {
        public DocumentationItemService Create()
        {
            return new DocumentationItemService();
        }
    }
}
