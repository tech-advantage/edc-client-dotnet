using edcClientDotnet;
using edcClientDotnet.model;

namespace edcClientDotnetTest.internalImpl
{
    [TestClass]
    public class EdcClientImplTest : CommonBase
    {
        private IEdcClient _edcClient;

        [TestInitialize]
        public void Setup()
        {
            _edcClient = CreateEdcClient();
        }

        [TestMethod]
        public void ShouldGetContextUrl()
        {
            String url = _edcClient.GetContextWebHelpUrl("fr.techad.edc", "help.center", "en");
            Assert.AreEqual("https://demo.easydoccontents.com/help/context/edchelp/fr.techad.edc/help.center/en/0", url);
        }

        [TestMethod]
        public void ShouldGetContextUrlWithRank()
        {
            String url = _edcClient.GetContextWebHelpUrl("fr.techad.edc", "help.center", 2, "en");
            Assert.AreEqual("https://demo.easydoccontents.com/help/context/edchelp/fr.techad.edc/help.center/en/2", url);
        }

        [TestMethod]
        public void ShouldGetContext()
        {
            IContextItem contextItem = _edcClient.GetContextItem("fr.techad.edc", "help.center", "en");
            Assert.IsNotNull(contextItem);
            Assert.AreEqual(1, contextItem.ArticleSize());
            Assert.AreEqual(3, contextItem.LinkSize());
        }

        [TestMethod]
        public void ShouldGetDocumentationUrl()
        {
            String documentationWebHelpUrl = _edcClient.GetDocumentationWebHelpUrl(434L, "ru", "myPluginId");
            Assert.AreEqual("https://demo.easydoccontents.com/help/doc/myPluginId/434/ru", documentationWebHelpUrl);
        }

        [TestMethod]
        public void ShouldGetDocumentationUrlWithNullLanguage()
        {
            String documentationWebHelpUrl = _edcClient.GetDocumentationWebHelpUrl(434L, null, "myPluginId");
            Assert.AreEqual("https://demo.easydoccontents.com/help/doc/myPluginId/434", documentationWebHelpUrl);
        }

        [TestMethod]
        public void ShouldGetDocumentationUrlWithNullPublicationId()
        {
            String documentationWebHelpUrl = _edcClient.GetDocumentationWebHelpUrl(434L, null, null);
            Assert.AreEqual("https://demo.easydoccontents.com/help/doc/434", documentationWebHelpUrl);
        }
    }
}
