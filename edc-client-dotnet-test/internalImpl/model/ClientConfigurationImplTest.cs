using edc_client_dotnet.model;

namespace edc_client_dotnet_test.internalImpl.model
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
            _clientConfiguration.SetServerUrl("http://localhost");
            _clientConfiguration.SetDocumentationContext("mydoc");
            Assert.AreEqual("http://localhost/mydoc", _clientConfiguration.GetDocumentationUrl());
        }

        [TestMethod]
        public void ShouldReturnTheDocumentationUrl()
        {
            _clientConfiguration.SetServerUrl("http://localhost/");
            _clientConfiguration.SetDocumentationContext("mydoc");
            Assert.AreEqual("http://localhost/mydoc", _clientConfiguration.GetDocumentationUrl());
        }

        [TestMethod]
        public void ShouldReturnTheDocumentationUrlWithDefaultDocumentationContext()
        {
            _clientConfiguration.SetServerUrl("http://localhost/");
            Assert.AreEqual("http://localhost/doc", _clientConfiguration.GetDocumentationUrl());
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrlAndAddSlash()
        {
            _clientConfiguration.SetServerUrl("http://localhost");
            _clientConfiguration.SetWebHelpContext("my-help");
            Assert.AreEqual("http://localhost/my-help", _clientConfiguration.GetWebHelpUrl());
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrl()
        {
            _clientConfiguration.SetServerUrl("http://localhost/");
            _clientConfiguration.SetWebHelpContext("my-help");
            Assert.AreEqual("http://localhost/my-help", _clientConfiguration.GetWebHelpUrl());
        }

        [TestMethod]
        public void ShouldReturnTheWebHelpUrlWithDefaultWebHelpContext()
        {
            _clientConfiguration.SetServerUrl("http://localhost/");
            Assert.AreEqual("http://localhost/help", _clientConfiguration.GetWebHelpUrl());
        }

        [TestMethod]
        public void ShouldGetServerUrl()
        {
            _clientConfiguration.SetServerUrl("http://localhost:8080");
            Assert.AreEqual("http://localhost:8080", _clientConfiguration.GetServerUrl());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUrlException),
        "The server url is not defined")]
        public void ShouldThrowExceptionWhenTheServerUrlIsNull()
        {
            _clientConfiguration.SetDocumentationContext("mydoc");
            _clientConfiguration.GetDocumentationUrl();
        }

        [TestMethod]
        public void ShouldGetWebHelpContext()
        {
            Assert.AreEqual("help", _clientConfiguration.GetWebHelpContext());
        }

        [TestMethod]
        public void ShouldGetDocumentationContext()
        {
            Assert.AreEqual("doc", _clientConfiguration.GetDocumentationContext());
        }
}
}
