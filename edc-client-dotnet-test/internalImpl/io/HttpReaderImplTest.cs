using edc_client_dotnet.internalImpl.util;
using edc_client_dotnet.io;
using edc_client_dotnet.model;
using edc_client_dotnet.utils;

namespace edc_client_dotnet_test.internalImpl.io
{

    [TestClass]
    public class HttpReaderImplTest : CommonBase
    {
        private IEdcReader? _edcReader;
        String _languageCode = "en";
        HashSet<String> _languagesCodes = new HashSet<String>();

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

            Dictionary<String, IContextItem> contextItemDictionary = _edcReader.GetContext();
            Assert.AreEqual(39, contextItemDictionary.Count);

            IContextItem contextItem = contextItemDictionary.GetValueOrDefault(keyUtil.GetKey("fr.techad.edc", "help.center", "en"));
            _languageCode = contextItem.GetLanguageCode();
            Assert.AreEqual("All you need about edc", contextItem.GetDescription());
            Assert.AreEqual("About edc", contextItem.GetLabel());
            Assert.AreEqual("en", contextItem.GetLanguageCode());
            Assert.AreEqual("edchelp/html/en/1/12523/index.html", contextItem.GetUrl());
            Assert.AreEqual(1, contextItem.ArticleSize());
            Assert.AreEqual(3, contextItem.LinkSize());
        }

        [TestMethod]
        public void ShouldGetLabelValueWithDefinedLanguage()
        {
            String articlesLabelEN = _edcReader.ReadLabel(_languagesCodes).GetTranslation("en", "labels", "articles", "edchelp");
            Assert.IsFalse(String.IsNullOrEmpty(articlesLabelEN));
            Assert.AreEqual("Need more...", articlesLabelEN);
        }

        [TestMethod]
        public void ShouldGetErrorValueWithDefinedLanguage()
        {

            String errorLabelEN = _edcReader.ReadLabel(_languagesCodes).GetTranslation("en", "errors", "failedData", "leftmenu.account");
            Assert.IsFalse(String.IsNullOrEmpty(errorLabelEN));
            Assert.AreEqual("An error occurred when fetching data ! \nCheck the brick keys provided to the EdcHelp component.", errorLabelEN);

            String errorLabelFR = _edcReader.ReadLabel(_languagesCodes).GetTranslation("fr", "errors", "failedData", "leftmenu.account");
            Assert.IsFalse(String.IsNullOrEmpty(errorLabelFR));
            Assert.AreEqual("Une erreur s'est produite lors de la récupération des données! \nVérifiez les clés de brique fournies au composant EdcHelp", errorLabelFR);
        }
    }
}
