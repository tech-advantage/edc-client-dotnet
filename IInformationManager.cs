using edc_client_dotnet.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet
{
    internal interface IInformationManager
    {
        /// <summary>
        ///     Load the information for every publication, from the info.json files
        /// </summary>
        /// <exception cref="IOException">if an IO Exception occurred while reading the file</exception>
        /// <exception cref="InvalidUrlException">if the file url was not valid</exception>
        void LoadInformation();

        /// <summary>
        ///     Force the reload of the publications information on the next read
        /// </summary>
        void ForceReload();

        /// <summary>
        ///     Return the information for each present publication
        /// </summary>
        /// <returns>a map with publication id as key and information as value</returns>
        /// <exception cref="IOException">if an error occurred while getting the file</exception>
        /// <exception cref="InvalidUrlException">if the url was not valid</exception>
        Dictionary<String, IInformation> GetPublicationInformation();
    }
}
