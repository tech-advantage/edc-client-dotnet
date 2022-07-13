using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class ObjectIdService : IObjectIdService
    {
        //private IObjectIdService _objectId;
        private long id;

        public long GetId(){ return this.id; }

        public void SetId(long id) { this.id = id; }
        
    }
}
