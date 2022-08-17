using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ContextItemImpl : DocumentationItemImpl, IContextItem
    {
        private String _description;
        private String _mainKey;
        private String _subKey;

        public ContextItemImpl() : base(DocumentationItemType.CONTEXTUAL){}
        
        public String GetDescription() { return _description; }

        public String GetMainKey() { return _mainKey; }

        public String GetSubKey() { return _subKey; }

        public void SetDescription(String description) { _description = description; }

        public void SetMainKey(String mainKey) { _mainKey = mainKey; }

        public void SetSubKey(String subKey) { _subKey = subKey; }
    }
}
