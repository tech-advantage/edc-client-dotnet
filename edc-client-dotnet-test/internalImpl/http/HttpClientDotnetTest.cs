namespace edcClientDotnetTest.internalImpl.http
{
    [TestClass]
    public class HttpClientDotnetTest
    {
        private HttpClient httpClient;
        HttpResponseMessage response;
        String result;

        [TestInitialize]
        public void Setup() { httpClient = new HttpClient(); }

        [TestMethod]
        public void ShouldGetAFile()
        {
            HttpResponseMessage response = httpClient.GetAsync("https://demo.easydoccontents.com/doc/edchelp/context.json").Result;
            Assert.IsNotNull(response);
            result = response.Content.ReadAsStringAsync().Result;
            Assert.IsFalse(String.IsNullOrEmpty(result));
        }
    }
}
