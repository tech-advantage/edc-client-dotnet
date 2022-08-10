using System.ComponentModel;

namespace edc_client_dotnet.model
{
    public enum I18NTranslation
    {
        /// <summary>
        ///     I18N Default language code
        /// </summary>
        [Description("en")]
        DEFAULT_LANGUAGE_CODE,

        /// <summary>
        ///     I18N Labels
        /// </summary>
        [Description("labels")]
        I18N_LABELS_ROOT,

        /// <summary>
        ///     I18N Errors
        /// </summary>
        [Description("errors")]
        I18N_ERRORS_ROOT,

        /// <summary>
        ///     I18N Articles
        /// </summary>
        [Description("articles")]
        ARTICLES_KEY,

        /// <summary>
        ///     I18N Links
        /// </summary>
        [Description("links")]
        LINKS_KEY,

        /// <summary>
        ///     I18N ComingSoon
        /// </summary>
        [Description("comingSoon")]
        COMING_SOON_KEY,

        /// <summary>
        ///     I18N Error title
        /// </summary>
        [Description("errorTitle")]
        ERROR_TITLE_KEY,

        /// <summary>
        ///     I18N Failed data
        /// </summary>
        [Description("failedData")]
        ERRORS_KEY
    }
}
