using edcClientDotnet.factory;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.factory
{
    public class DocumentationItemFactory : IDocumentationItemFactory
    {
        public IDocumentationItem Create()
        {
            return new DocumentationItemImpl();
        }
    }
}
