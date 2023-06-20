using edcClientDotnet.model;
using edcClientDotnet.utils;
using System.Collections;

namespace edcClientDotnet.internalImpl
{
    public class TranslationConstants : IEnumerable<String>
    {
        public static Dictionary<String, String> GetDefaultLabels()
        {
            return new Dictionary<string, string>
            {
                [ParseEnumDescription.GetDescription(I18NTranslation.ARTICLES_KEY)] = "Need more...",
                [ParseEnumDescription.GetDescription(I18NTranslation.LINKS_KEY)] = "Related topics",
                [ParseEnumDescription.GetDescription(I18NTranslation.COMING_SOON_KEY)] = "Contextual help is coming soon.",
                [ParseEnumDescription.GetDescription(I18NTranslation.ERROR_TITLE_KEY)] = "Error",
            };
        }

        public static Dictionary<string, string> GetDefaultErrors()
        {
            return new Dictionary<string, string>
            {
                [ParseEnumDescription.GetDescription(I18NTranslation.ERRORS_KEY)] = "An error occurred when fetching data!\nCheck the brick keys provided to the EdcHelp component."
            };
        }

        public static readonly IEnumerable<String> LANGUAGES_CODES = new HashSet<String>
        {
            "en", // English
            "ar", // Arabic
            "bg", // Bulgarian
            "zh", // Chinese
            "hr", // Croatian
            "cs", // Czech
            "da", // Danish
            "nl", // Dutch
            "et", // Estonian
            "fi", // Finnish
            "fr", // French
            "de", // German
            "el", // Greek
            "he", // Hebrew
            "hu", // Hungarian
            "is", // Icelandic
            "ga", // Irish
            "it", // Italian
            "ja", // Japanese
            "ko", // Korean
            "lv", // Latvian
            "lt", // Lithuanian
            "lb", // Luxembourgish
            "mt", // Maltese
            "no", // Norwegian
            "fa", // Persian
            "pl", // Polish
            "pt", // Portuguese
            "ro", // Romanian
            "ru", // Russian
            "sk", // Slovak
            "sl", // Slovenian
            "es", // Spanish
            "sv", // Swedish
            "tr", // Turkish
            "vi"  // Vietnamese
        };

        public IEnumerator<String> GetEnumerator()
        {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }
    }
}
