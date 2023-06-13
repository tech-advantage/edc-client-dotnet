using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ContextItemImpl : DocumentationItemImpl, IContextItem
    {
        private String _description;
        private String _mainKey;
        private String _subKey;

        public ContextItemImpl() : base(DocumentationItemType.CONTEXTUAL) { }

        public String Description
        {
            get => _description;
            set => _description = value;
        }

        public String MainKey
        {
            get => _mainKey;
            set => _mainKey = value;
        }

        public String SubKey
        {
            get => _subKey;
            set => _subKey = value;
        }
    }
}
