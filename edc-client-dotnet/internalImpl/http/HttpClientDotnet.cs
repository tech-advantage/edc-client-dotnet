
namespace edc_client_dotnet.internalImpl.http
{
    public class HttpClientDotnet
    {
        public String GetResponseFromURI(String u)
        {
            String result = "";
            using (var client = new HttpClient())
            {
                try
                {
                    result = client.GetStringAsync(u).Result;
                }
                catch(Exception ex)
                {
                     Console.WriteLine(ex.Message);
                }
                
            }
            return result;
        }
    }
}
