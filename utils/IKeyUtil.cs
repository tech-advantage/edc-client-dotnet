using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet.utils
{
    internal interface IKeyUtil
    {
        /// <summary>
        ///     Build the key according to the main, sub key and the language code.
        /// </summary>
        /// <param name="mainKey"></param>
        /// <param name="subKey"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        String getKey(String mainKey, String subKey, String languageCode);

        /// <summary>
        ///     Check if the full key contains the main and subKey
        /// </summary>
        /// <param name="fullKey">the full key, containing keys and language code</param>
        /// <param name="mainKey">a main key</param>
        /// <param name="subKey">a sub key</param>
        /// <returns>returns true if mainKey.subKey is present for any language</returns>
        Boolean containsKey(String fullKey, String mainKey, String subKey);
    }
}
