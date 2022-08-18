using edcClientDotnet.factory.model;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory.impl
{
    public class ContextItemFactory : IContextItemFactory
    {
        public IContextItem Create()
        {
            return new ContextItemImpl();
        }
    }
}
