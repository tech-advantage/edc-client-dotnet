using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory
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
