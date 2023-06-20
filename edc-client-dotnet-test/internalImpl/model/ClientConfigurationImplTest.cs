using edcClientDotnet.model;

namespace edcClientDotnetTest.internalImpl.model
{
    [TestClass]
    public class ClientConfigurationImplTest : CommonBase
    {
        private IClientConfiguration _clientConfiguration;

        [TestInitialize]
        public void Setup()
        {
            _clientConfiguration = CreateClientConfig();
        }

        [TestMethod]
        public void ShouldReturnTheDocumentationUrlAndAddSlash()
        {
            _clientConfiguration.ServerUrl = "http://localhost";
            _clientConfiguration.DocumentationContext = "mydoc";
            Assert.AreEqual("http://localhost/mydoc", _clientConfiguration.DocumentationUrl);
        }

        [TestMethod]
        public void ShouldReturnTheDocumentationUrl()
        {
            _clientConfiguration.ServerUrl = "http://localhost/";
            _clientConfiguration.DocumentationContext = "mydoc";
            Assert.AreEqual("http://localhost/mydoc", _clientConfiguration.DocumentationUrl);
        }

        [TestMethod]
        public void ShouldReturnTheDocumentationUrlWithDefaultDocumentationContext()
        {
            _clientConfiguration.ServerUrl = "http://localhost/";
            Assert.AreEqual("http://localhost/doc", _clientConfiguration.DocumentationUrl);
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrlAndAddSlash()
        {
            _clientConfiguration.ServerUrl = "http://localhost";
            _clientConfiguration.WebHelpContext = "my-help";
            Assert.AreEqual("http://localhost/my-help", _clientConfiguration.WebHelpUrl);
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrl()
        {
            _clientConfiguration.ServerUrl = "http://localhost/";
            _clientConfiguration.WebHelpContext = "my-help";
            Assert.AreEqual("http://localhost/my-help", _clientConfiguration.WebHelpUrl);
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrlWithDefaultWebHelpContext()
        {
            _clientConfiguration.ServerUrl = "http://localhost/";
            Assert.AreEqual("http://localhost/help", _clientConfiguration.WebHelpUrl);
        }

        [TestMethod]
        public void ShouldGetServerUrl()
        {
            _clientConfiguration.ServerUrl = "http://localhost:8080";
            Assert.AreEqual("http://localhost:8080", _clientConfiguration.ServerUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUrlException),
        "The server url is not defined")]
        public void ShouldThrowExceptionWhenTheServerUrlIsNull()
        {
            _clientConfiguration.DocumentationContext = "mydoc";
            Assert.Fail(_clientConfiguration.DocumentationUrl);
        }

        [TestMethod]
        public void ShouldGetWebHelpContext()
        {
            Assert.AreEqual("help", _clientConfiguration.WebHelpContext);
        }

        [TestMethod]
        public void ShouldGetDocumentationContext()
        {
            Assert.AreEqual("doc", _clientConfiguration.DocumentationContext);
        }
}
}
