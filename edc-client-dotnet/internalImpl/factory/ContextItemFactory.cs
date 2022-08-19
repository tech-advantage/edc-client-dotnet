using edcClientDotnet.factory;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.factory
{
    public class ContextItemFactory : IContextItemFactory
    {
        public IContextItem Create()
        {
            return new ContextItemImpl();
        }
    }
}
