using edc_client_dotnet.utils;

namespace edc_client_dotnet_test.internalImpl.util
{
    [TestClass]
    public class UrlUtilImplTest : CommonBase
    {
        [TestMethod]
        public void ShouldGetHomeUrl()
        {
            IUrlUtil urlUtil = CreateUrlBuilder();
            String url = urlUtil.GetHomeUrl();
            Assert.AreEqual("https://demo.easydoccontents.com/help/home", url);
        }

        [TestMethod]
        public void ShouldGetErrorUrl()
        {
            IUrlUtil urlUtil = CreateUrlBuilder();
            String url = urlUtil.GetErrorUrl();
            Assert.AreEqual("https://demo.easydoccontents.com/help/error", url);
        }

        [TestMethod]
        public void ShouldCreateAContextUrl()
        {
            IUrlUtil urlUtil = CreateUrlBuilder();
            String url = urlUtil.GetContextUrl("fr.techad.edc.help", "fr.techad.edc", "help.center", "en", 0);
            Assert.AreEqual("https://demo.easydoccontents.com/help/context/fr.techad.edc.help/fr.techad.edc/help.center/en/0", url);
        }

        [TestMethod]
        public void ShouldCreateADocUrl()
        {
            IUrlUtil urlUtil = CreateUrlBuilder();
            String url = urlUtil.GetDocumentationUrl(12L, "fr", "myPluginId");
            Assert.AreEqual("https://demo.easydoccontents.com/help/doc/myPluginId/12/fr", url);
        }

        [TestMethod]
        public void ShouldCreateADocUrlWithNullLanguage()
        {
            IUrlUtil urlUtil = CreateUrlBuilder();
            String url = urlUtil.GetDocumentationUrl(12L, null, "myPluginId");
            Assert.AreEqual("https://demo.easydoccontents.com/help/doc/myPluginId/12", url);
        }
    }
}
