namespace edcClientDotnet.utils
{
    public interface IKeyUtil
    {
        /// <summary>
        ///     Build the key according to the main, sub key and the language code.
        /// </summary>
        /// <param name="mainKey"></param>
        /// <param name="subKey"></param>
        /// <param name="languageCode"></param>
        /// <returns>the key</returns>
        String GetKey(String mainKey, String subKey, String languageCode);

        /// <summary>
        ///     Check if the full key contains the main and subKey
        /// </summary>
        /// <param name="fullKey">the full key, containing keys and language code</param>
        /// <param name="mainKey">a main key</param>
        /// <param name="subKey">a sub key</param>
        /// <returns>returns true if mainKey.subKey is present for any language</returns>
        Boolean ContainsKey(String fullKey, String mainKey, String subKey);
    }
}
