using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory
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
