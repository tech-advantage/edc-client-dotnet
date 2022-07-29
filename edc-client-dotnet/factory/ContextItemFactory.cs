using edc_client_dotnet.internalImpl.model;

namespace edc_client_dotnet.factory
{
    public class ContextItemFactory
    {
        public ContextItemService Create()
        {
            return new ContextItemService();
        }
    }
}
