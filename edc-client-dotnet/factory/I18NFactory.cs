using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
{
    public class I18NFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public I18NFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public II18NContent Create()
        {
            return (II18NContent)_serviceProvider.GetService(typeof(I18NContentImpl));
        }
    }
}
