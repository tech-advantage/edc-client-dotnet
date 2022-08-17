using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory
{
    public class ContextItemFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ContextItemFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IContextItem Create()
        {
            return (IContextItem)_serviceProvider.GetService(typeof(ContextItemImpl));
        }
    }
}
