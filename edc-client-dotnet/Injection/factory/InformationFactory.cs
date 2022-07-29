using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.Injection.factory
{
    public class InformationFactory
    {
        public IInformation Create()
        {
            return new InformationImpl();
        }
    }
}
