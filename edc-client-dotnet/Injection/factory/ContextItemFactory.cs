using edc_client_dotnet.internalImpl.model;
using edc_client_dotnet.model;

namespace edc_client_dotnet.factory
{
    public class ContextItemFactory
    {
        public IContextItem Create()
        {
            return new ContextItemImpl();
        }
    }
}
