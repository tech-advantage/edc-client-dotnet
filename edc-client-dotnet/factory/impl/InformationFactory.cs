using edcClientDotnet.factory.model;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory.impl
{
    public class InformationFactory : IInformationFactory
    {
        public IInformation Create()
        {
            return new InformationImpl();
        }
    }
}
