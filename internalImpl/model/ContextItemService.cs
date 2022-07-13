using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class ContextItemService : DocumentationItemService, IContextItemService
    {
        //private IContextItem _contextItem;
        private String description;
        private String mainKey;
        private String subKey;

        public ContextItemService() : base(DocumentationItemType.CONTEXTUAL){}

        
        public string GetDescription()
        {
            return description;
        }

        public string GetMainKey()
        {
            return mainKey;
        }

        public string GetSubKey()
        {
            return subKey;
        }

        public void SetDescription(string description)
        {
            this.description = description;
        }

        public void SetMainKey(string mainKey)
        {
            this.mainKey = mainKey;
        }

        public void SetSubKey(string subKey)
        {
            this.subKey = subKey;
        }
    }
}
