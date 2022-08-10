using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
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
