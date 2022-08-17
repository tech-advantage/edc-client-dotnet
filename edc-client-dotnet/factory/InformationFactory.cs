using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.Injection.factory
{
    public class InformationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public InformationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IInformation Create()
        {
            return (IInformation)_serviceProvider.GetService(typeof(InformationImpl));
        }
    }
}
