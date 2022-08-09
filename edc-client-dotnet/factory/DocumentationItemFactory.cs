using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
{
    public class DocumentationItemFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DocumentationItemFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IDocumentationItem Create()
        {
            return (IDocumentationItem)_serviceProvider.GetService(typeof(DocumentationItemImpl));
        }
    }
}
