using edc_client_dotnet.model;
using edc_client_dotnet.utils;

namespace edc_client_dotnet.internalImpl.io
{
    internal class HttpReaderImpl
    {
        private static readonly String MUTLI_DOC_FILE = "multi-doc.json";
        private static readonly String CONTEXT_FILE = "context.json";
        private static readonly String INFO_FILE = "info.json";

        private static readonly String POPOVER_I18N_PATH = "i18n/popover/";
        private static readonly String I18N_FILE_EXTENSION = ".json";

        private HttpClient httpClient;
        private IClientConfiguration clientConfiguaration;
        private IKeyUtil keyUtil;
    }
}
