using edcClientDotnet.model;

namespace edcClientDotnet.internalImpl.model
{
    public class ObjectIdImpl : IObjectId
    {
        private long _id;

        public long ObjectId
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
