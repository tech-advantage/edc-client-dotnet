using edcClientDotnet;
using edcClientDotnet.model;
using Newtonsoft.Json;
using edcClientDotnetTest.internalImpl;

namespace edcClientDotnetTest;

[TestClass]
public class EdcClientSingletonTest
{
    private static string DEFAULT_URL = "https://demo.easydoccontents.com/help/home";

    [TestInitialize]
    public void Setup()
    {
        EdcClientSingleton.GetInstance().SetServerUrl(Constants.SERVER_URL);
        try
        {
            EdcClientSingleton.GetInstance().SetWebHelpContextUrl("help");
            EdcClientSingleton.GetInstance().SetDocumentationContextUrl("doc");
        }
        catch (InvalidUrlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    [TestMethod]
    public void ShouldGetUrl()
    {
        EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
        Assert.AreEqual("https://demo.easydoccontents.com/help/context/edchelp/fr.techad.edc/help.center/en/0", url);
    }

    [TestMethod]
    public void ShouldGetUrlWithForceReload()
    {
        EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
        Assert.AreEqual("https://demo.easydoccontents.com/help/context/edchelp/fr.techad.edc/help.center/en/0", url);
    }

    [TestMethod]
    public void ShouldGetDefaultUrlErrorOnMainKey()
    {
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl(null, "help.center", "en");
        Assert.AreEqual(DEFAULT_URL, url);
    }

    [TestMethod]
    public void ShouldGetNullUrlErrorOnSubKey()
    {
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", null, "en");
        Assert.AreEqual(DEFAULT_URL, url);
    }

    [TestMethod]
    public void ShouldGetDefaultUrlOnLanguageCode()
    {
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", null);
        Assert.AreEqual(DEFAULT_URL, url);
    }

    [TestMethod]
    public void ShouldGetUrlWithNewWebHelpContext()
    {
        EdcClientSingleton.GetInstance().SetWebHelpContextUrl("help");
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
        Assert.AreEqual("https://demo.easydoccontents.com/help/context/edchelp/fr.techad.edc/help.center/en/0", url);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidUrlException),
    "The WebHelp context is null")]
    public void ShouldThrowsExceptionOnNullWebHelpContext()
    {
        EdcClientSingleton.GetInstance().SetWebHelpContextUrl(null);
    }

    [TestMethod]
    [ExpectedException(typeof(JsonReaderException))]
    public void ShouldThrowsExceptionOnUnknownDocumentationContext()
    {
        EdcClientSingleton.GetInstance().SetDocumentationContextUrl("my-doc");
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidUrlException))]
    public void ShouldThrowsExceptionOnNullServer()
    {
        EdcClientSingleton.GetInstance().SetServerUrl(null);
        EdcClientSingleton.GetInstance().ForceReload();
        String url = EdcClientSingleton.GetInstance().GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
    }
}