﻿using edc_client_dotnet.model;
using edc_client_dotnet.utils;
using static edc_client_dotnet.model.I18NTranslationService;

namespace edc_client_dotnet.internalImpl.model
{

    public class InformationService : IInformationService
    {
        private IInformationService _information;
        private String defaultLanguage = ParseEnumDescription.GetDescription(DEFAULT_LANGUAGE_CODE);
        private HashSet<String> languages;

        public string GetDefaultLanguage()
        {
            return defaultLanguage;
        }
        
        public HashSet<string> GetLanguages()
        {
            return languages;
        }

        public void SetDefaultLanguage(string defaultLanguage)
        {
            this.defaultLanguage = defaultLanguage;
        }

        public void SetLanguages(HashSet<string> languages)
        {
            this.languages = languages;
        }
    }
}