
namespace edc_client_dotnet.model
{
    public interface II18NContentService
    {
        /// <summary>
        ///     Return I18n translation built with the lang and key
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="publicationId"></param>
        /// <returns>translation</returns>
        String GetTranslation(String lang, String type, String key, String publicationId);

        /// <summary>
        ///     Set content to build key
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetMessage(String lang, String type, String key, String value);

        /// <summary>
        ///     Set I18n labels
        /// </summary>
        /// <param name="i18nLabel"></param>
        void SetI18nLabel(Dictionary<String, String> i18nLabel);

        /// <summary>
        ///     Set I18n errors
        /// </summary>
        /// <param name="i18nError"></param>
        void SetI18nError(Dictionary<String, String> i18nError);

        /// <summary>
        ///     Get the label
        /// </summary>
        /// <returns>The label</returns>
        Dictionary<String, String> GetLabel();

        /// <summary>
        ///     Get the error
        /// </summary>
        /// <returns>The error</returns>
        Dictionary<String, String> GetError();
    }
}
