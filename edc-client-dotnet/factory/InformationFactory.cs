using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.Injection.factory
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
