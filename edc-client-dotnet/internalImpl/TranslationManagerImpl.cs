using edcClientDotnet.io;
using edcClientDotnet.model;
using edcClientDotnet.utils;
using System.Collections.ObjectModel;

namespace edcClientDotnet.internalImpl
{
    public class TranslationManagerImpl : ITranslationManager
    {
        private readonly IEdcReader _reader;
        private readonly ITranslationUtil _translationUtil;
        private II18NContent? _translation = null;
        // The languages codes present among all the publications
        private readonly HashSet<String> _languageCodes = new();
        // The default language code for each publication id
        private readonly Dictionary<String, String> _defaultPublicationLanguages = new();
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public TranslationManagerImpl(IEdcReader reader, ITranslationUtil translationUtil)
        {
            _reader = reader;
            _translationUtil = translationUtil;
        }

        /// <exception cref="IOException"></exception>
        /// <exception cref="InvalidUrlException"></exception>
        public void LoadTranslations(ReadOnlyDictionary<String, IInformation> publicationInformation)
        {
            if(_translation == null && publicationInformation != null)
            {
                foreach (var entry in publicationInformation)
                {
                    IInformation information = entry.Value;
                    AddToDefaultLanguages(entry.Key, information);
                    AddToLanguages(information);
                }
            }
            _translation = _reader.ReadLabel(_languageCodes);
        }

        public ReadOnlyDictionary<String, String> GetDefaultPublicationLanguages()
        {
            return new ReadOnlyDictionary<String, String>(_defaultPublicationLanguages);
        }

        public String GetLabel(String labelKey, String languageCode, String publicationId)
        {
            _logger.Debug("Getting label with key {}, for languageCode {} and publicationId {}", labelKey, languageCode, publicationId);
            String translationLabelValue = GetTranslation(languageCode, ParseEnumDescription.GetDescription(I18NTranslation.I18N_LABELS_ROOT), labelKey, publicationId);
            return String.IsNullOrEmpty(translationLabelValue) ? translationLabelValue : TranslationConstants.GetDefaultLabels().GetValueOrDefault(labelKey);
        }

        public String GetError(String errorKey, String languageCode, String publicationId)
        {
            _logger.Debug("Getting error with key {}, for languageCode {} and publicationId {}", errorKey, languageCode, publicationId);
            String translationErrorValue = GetTranslation(languageCode, ParseEnumDescription.GetDescription(I18NTranslation.I18N_ERRORS_ROOT), errorKey, publicationId);
            return String.IsNullOrEmpty(translationErrorValue) ? translationErrorValue : TranslationConstants.GetDefaultErrors().GetValueOrDefault(errorKey);
        }

        public void ForceReload()
        {
            _translation = null;
            _languageCodes.Clear();
            _defaultPublicationLanguages.Clear();
        }

        private String GetTranslation(String lang, String type, String key, String publicationId)
        {
            _logger.Debug("Getting translation with lang {}, type {}, for key {} and publicationId {}", lang, type, key, publicationId);
            return _translation.GetTranslation(lang, type, key, publicationId);
        }

        private void AddToDefaultLanguages(String publicationId, IInformation information)
        {
            if (information != null && _translationUtil.IsLanguageCodeValid(information.GetDefaultLanguage()))
            {
                _defaultPublicationLanguages.Add(publicationId, information.GetDefaultLanguage());
            }
        }

        private void AddToLanguages(IInformation information)
        {
            if (information != null && information.GetLanguages() != null)
            {
                foreach (String languageCode in information.GetLanguages())
                {
                    if(_translationUtil.IsLanguageCodeValid(languageCode))
                        _languageCodes.Add(languageCode);
                }
            }
        }
    }
}
