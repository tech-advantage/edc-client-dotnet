using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ObjectIdImpl : IObjectId
    {
        private long _id;

        public long GetId(){ return _id; }

        public void SetId(long id) { _id = id; }
        
    }
}
