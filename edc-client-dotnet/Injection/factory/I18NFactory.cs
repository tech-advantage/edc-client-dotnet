using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
{
    public class I18NFactory
    {
        public II18NContent Create()
        {
            return new I18NContentImpl();
        }
    }
}
