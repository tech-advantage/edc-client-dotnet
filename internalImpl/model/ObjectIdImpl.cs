using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    internal class ObjectIdImpl : IObjectId
    {
        private long id;

        public long GetId(){ return this.id; }

        public void SetId(long id) { this.id = id; }
        
    }
}
