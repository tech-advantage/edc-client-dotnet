using edcClientDotnet.model;

namespace edcClientDotnetTest.internalImpl.model
{
    [TestClass]
    public class I18NContentImplTest : CommonBase
    {
        private II18NContent _i18nContent;

        [TestInitialize]
        public void Setup()
        {
            _i18nContent = CreateIi8nContent();
        }
        

        [TestMethod]
        public void ShouldAddTranslation()
        {
            _i18nContent.SetTranslation("en", "labels", "articles", "Need more...");
        }

        [TestMethod]
        public void ShouldGetTranslation()
        {
            _i18nContent.SetTranslation("en", "labels", "articles", "Need more...");
            String test = _i18nContent.GetTranslation("en", "labels", "articles", "webmailmain");
            Assert.AreEqual(test, "Need more...");
        }
    }
}
