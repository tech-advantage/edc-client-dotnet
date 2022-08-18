using edcClientDotnet.factory.model;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory.impl
{
    public class I18NFactory : II18NFactory
    {
        public II18NContent Create()
        {
            return new I18NContentImpl();
        }
    }
}
