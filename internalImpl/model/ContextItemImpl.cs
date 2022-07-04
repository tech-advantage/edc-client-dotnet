using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    internal class ContextItemImpl : DocumentationItemImpl, IContextItem
    {
        private String description;
        private String mainKey;
        private String subKey;

        public ContextItemImpl(): base(DocumentationItemType.CONTEXTUAL) {}
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
