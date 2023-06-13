using edcClientDotnet.factory;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.factory
{
    public class I18NFactory : II18NFactory
    {
        public II18NContent Create()
        {
            return new I18NContentImpl();
        }
    }
}
