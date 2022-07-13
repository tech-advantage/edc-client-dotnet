
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
    }
}
