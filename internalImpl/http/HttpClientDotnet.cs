using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet.internalImpl.http
{
    internal class HttpClientDotnet
    {
        public async Task<string> GetResponseFromURI(Uri u)
        {
            var response = "";
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage result = await client.GetAsync(u);
                    if (result.IsSuccessStatusCode)
                    {
                        response = await result.Content.ReadAsStringAsync();
                    }
                }
                catch
                {

                }
                
            }
            return response;
        }
    }
}
