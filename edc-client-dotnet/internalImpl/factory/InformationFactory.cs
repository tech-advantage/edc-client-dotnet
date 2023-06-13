using edcClientDotnet.factory;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.factory
{
    public class InformationFactory : IInformationFactory
    {
        public IInformation Create()
        {
            return new InformationImpl();
        }
    }
}
