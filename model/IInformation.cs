
namespace edc_client_dotnet.model
{
    internal interface IInformation
    {
        String GetDefaultLanguage();

        void SetDefaultLanguage(String defaultLanguage);

        HashSet<String> GetLanguages();

        void SetLanguages(HashSet<String> languages);
    }
}
