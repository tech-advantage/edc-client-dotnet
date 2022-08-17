namespace edcClientDotnet.model
{
    public interface II18NContent
    {
        /// <summary>
        ///     Return I18n translation built with the lang and key
        /// </summary>
        /// <param name="lang">the lang i18n translation</param>
        /// <param name="type">the label type</param>
        /// <param name="key">the label key</param>
        /// <param name="publicationId">the publication id</param>
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
