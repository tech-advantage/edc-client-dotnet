using edc_client_dotnet.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edc_client_dotnet
{
    public class EdcClientSingleton : IEdcClient
    {
        private static EdcClientSingleton instance = null;
        private IEdcClient edcClient;

        private EdcClientSingleton() : base() { }

        //public static EdcClientSingleton GetInstance()
        //{
        //    if(instance == null)
        //    {
        //        instance = new EdcClientSingleton();
        //        instance.init()
        //    }
        //}
        public void ForceReload()
        {
            throw new NotImplementedException();
        }

        public IContextItemService GetContextItem(string mainKey, string subKey, string languageCode)
        {
            throw new NotImplementedException();
        }

        public string GetContextWebHelpUrl(string mainKey, string subKey, string languageCode)
        {
            throw new NotImplementedException();
        }

        public string GetContextWebHelpUrl(string mainKey, string subKey, int rank, string languageCode)
        {
            throw new NotImplementedException();
        }

        public string GetDocumentationWebHelpUrl(long id, string languageCode, string srcPublicationId)
        {
            throw new NotImplementedException();
        }

        public string GetError(string errorKey, string languageCode, string publicationId)
        {
            throw new NotImplementedException();
        }

        public string GetLabel(string labelKey, string languageCode, string publicationId)
        {
            throw new NotImplementedException();
        }

        public void loadContext()
        {
            throw new NotImplementedException();
        }

        public void setDocumentationContextUrl(string documentationContextUrl)
        {
            throw new NotImplementedException();
        }

        public void setServerUrl(string serverUrl)
        {
            throw new NotImplementedException();
        }

        public void SetWebHelpContextUrl(string webHelpContextUrl)
        {
            throw new NotImplementedException();
        }
    }
}
