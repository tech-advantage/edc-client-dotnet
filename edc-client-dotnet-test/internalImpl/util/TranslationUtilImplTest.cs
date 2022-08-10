﻿using edc_client_dotnet.Injection;
using edc_client_dotnet.Injection.factory;
using edc_client_dotnet.model;
using edc_client_dotnet.utils;
using Microsoft.Extensions.DependencyInjection;

namespace edc_client_dotnet_test.internalImpl.util
{
    [TestClass]
    public class TranslationUtilImplTest : CommonBase
    {
        [TestMethod]
        public void ShouldGetPublicationDefaultLanguages()
        {
            ITranslationUtil translationUtil = CreateTranslationUtil();
            IInformation information1 = CreateInformation("fr");
            IInformation information2 = CreateInformation("ru");

            SortedDictionary<String, IInformation> informationPerPublication = new SortedDictionary<String, IInformation>();
            informationPerPublication.Add("pub1", information1);
            informationPerPublication.Add("pub2", information2);
            SortedDictionary<String, String> defaultLanguages = translationUtil.GetPublicationDefaultLanguages(informationPerPublication);
            SortedDictionary<String, String> infoTest = new SortedDictionary<String, String>
            {
                { "pub1", "fr" },
                { "pub2", "ru" }
            };
            CollectionAssert.AreEqual(infoTest, defaultLanguages);
        }

        [TestMethod]
        public void ShouldCheckIfLanguageCodeIsValid()
        {
            ITranslationUtil translationUtil = CreateTranslationUtil();
            Assert.IsTrue(translationUtil.IsLanguageCodeValid("en"));
            Assert.IsTrue(translationUtil.IsLanguageCodeValid("fr"));
            Assert.IsFalse(translationUtil.IsLanguageCodeValid(""));
            Assert.IsFalse(translationUtil.IsLanguageCodeValid("abc"));
            Assert.IsFalse(translationUtil.IsLanguageCodeValid(" fr"));
            Assert.IsFalse(translationUtil.IsLanguageCodeValid("fr "));
            Assert.IsFalse(translationUtil.IsLanguageCodeValid(null));
        }

        [TestMethod]
        public void ShouldCheckValidTranslatedLabels()
        {
            ITranslationUtil translationUtil = CreateTranslationUtil();
            Dictionary<String, String> labelsToCheck = CreateLabels("my articles label", "my links label");
            Boolean isValid = translationUtil.CheckTranslatedLabels(labelsToCheck);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ShouldCheckInvalidTranslatedLabels()
        {
            ITranslationUtil translationUtil = CreateTranslationUtil();
            Dictionary<String, String> labelsToCheck = CreateLabels("my articles label", null);
            Boolean isValid = translationUtil.CheckTranslatedLabels(labelsToCheck);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void shouldCheckEmptyTranslatedLabels()
        {
            ITranslationUtil translationUtil = CreateTranslationUtil();
            Dictionary<String, String> labelsToCheck = CreateLabels("my articles label", "");
            Boolean isValid = translationUtil.CheckTranslatedLabels(labelsToCheck);
            Assert.IsFalse(isValid);
        }

        private IInformation CreateInformation(String defaultLanguageCode)
        {
            InformationFactory informationFactory = Startup.serviceProvider.GetService<InformationFactory>();
            IInformation information = informationFactory.Create();
            information.SetDefaultLanguage(defaultLanguageCode);
            return information;
        }

        public Dictionary<String, String> CreateLabels(String articlesLabel, String linksLabels)
        {
            Dictionary<String, String> labels = new Dictionary<String, String>();
            if (articlesLabel != null)
                labels.Add(ParseEnumDescription.GetDescription(I18NTranslation.ARTICLES_KEY), articlesLabel);
            if (linksLabels is not null)
                labels.Add(ParseEnumDescription.GetDescription(I18NTranslation.LINKS_KEY), linksLabels);

            return labels;
        }
    }

}