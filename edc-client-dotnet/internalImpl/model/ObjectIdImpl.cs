using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.model
{
    public class ObjectIdImpl : IObjectId
    {
        private long _id;

        public long GetId(){ return _id; }

        public void SetId(long id) { _id = id; }
        
    }
}
