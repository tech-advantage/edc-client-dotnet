using edcClientDotnet.utils;

namespace edcClientDotnet.internalImpl.util
{
    public class KeyUtilImpl : IKeyUtil
    {
        public String GetKey(String mainKey, String subKey, String languageCode)
        {
            return mainKey + "." + subKey + "." + languageCode;
        }

        public bool ContainsKey(String fullKey, String mainKey, String subKey)
        {
            return !String.IsNullOrEmpty(fullKey) && !String.IsNullOrEmpty(mainKey) && !String.IsNullOrEmpty(subKey) && fullKey.Contains(mainKey + "." + subKey);
        }

    }
}
