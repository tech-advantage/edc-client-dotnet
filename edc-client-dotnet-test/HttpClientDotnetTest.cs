namespace edc_client_dotnet_test;

[TestClass]
public class HttpClientDotnetTest
{

    private HttpClient httpClient;

    [TestInitialize]
    public void setup()
    {
        httpClient = new HttpClient();
    }

    [TestMethod]
    public void ShouldGetAfile()
    {
        Task txt = httpClient.GetAsync("https://demo.easydoccontents.com/doc/edchelp/context.json");
        Assert.IsFalse(txt.IsCanceled);
    }
}