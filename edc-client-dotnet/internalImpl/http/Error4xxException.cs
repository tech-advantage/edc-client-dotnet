namespace edcClientDotnet.internalImpl.http
{
    public class Error4xxException : Exception
    {
        public Error4xxException(String message) : base(message) { }
    }
}
