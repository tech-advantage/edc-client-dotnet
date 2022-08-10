using edc_client_dotnet.model;

namespace edc_client_dotnet.internalImpl.http
{
    public class HttpClientDotnet : HttpClient
    {
        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public String GetAsyncResponse(String url)
        {
            HttpResponseMessage response;
            String result;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = client.GetAsync(url).Result;
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 400 && statusCode < 500)
                    {
                        throw new Error4xxException("The url '" + url + "' return " + response.StatusCode);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        return result;
                    }
                }
                catch(Exception ex)
                {
                    throw new Error4xxException(ex.Message);
                }
                
            }
            return null;
        }
    }
}
