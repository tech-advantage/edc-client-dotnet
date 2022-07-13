using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
