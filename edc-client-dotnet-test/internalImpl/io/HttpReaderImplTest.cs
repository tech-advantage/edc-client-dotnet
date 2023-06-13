using edcClientDotnet.internalImpl.util;
using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using NLog;

namespace edcClientDotnetTest.internalImpl.io
{
    [TestClass]
    public class HttpReaderImplTest : CommonBase
    {
        private IEdcReader? _edcReader;
        String _languageCode = "en";
        HashSet<String> _languagesCodes = new HashSet<String>();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [TestInitialize]
        public void Setup()
        {
            _languagesCodes.Add("en");
            _languagesCodes.Add("fr");
            _edcReader = CreateEdcReader();
        }

        [TestMethod]
        public void ShouldGetContextJson()
        {
            IKeyUtil keyUtil = new KeyUtilImpl();

            Dictionary<String, IContextItem> contextItemDictionary = _edcReader.ReadContext();
            Assert.AreEqual(39, contextItemDictionary.Count);

            IContextItem contextItem = contextItemDictionary.GetValueOrDefault(keyUtil.GetKey("fr.techad.edc", "help.center", "en"));
            _languageCode = contextItem.LanguageCode;
            Assert.AreEqual("All you need about edc", contextItem.Description);
            Assert.AreEqual("About edc", contextItem.Label);
            Assert.AreEqual("en", contextItem.LanguageCode);
            Assert.AreEqual("edchelp/html/en/1/12523/index.html", contextItem.Url);
            Assert.AreEqual(1, contextItem.ArticleSize());
            Assert.AreEqual(3, contextItem.LinkSize());
        }

        

        [TestMethod]
        public void ShouldGetErrorValueWithDefinedLanguage()
        {

            //String errorLabelEN = _edcReader.ReadLabel(_languagesCodes).GetTranslation("en", "errors", "failedData", "leftmenu.account");
            //Assert.IsFalse(String.IsNullOrEmpty(errorLabelEN));
            //Assert.AreEqual("An error occurred when fetching data !\nCheck the brick keys provided to the EdcHelp component.", errorLabelEN);

            String errorLabelFR = _edcReader.ReadLabel(_languagesCodes).GetTranslation("fr", "errors", "failedData", "webmailmain");
            Assert.IsFalse(String.IsNullOrEmpty(errorLabelFR));
            Assert.AreEqual("Une erreur est survenue lors de la récupération des données !\nVérifiez les clés de la brique fournies au composant EdcHelp.", errorLabelFR);
        }
    }
}
