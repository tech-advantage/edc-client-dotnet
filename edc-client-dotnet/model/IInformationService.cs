namespace edc_client_dotnet.model
{
    public interface IInformationService
    {
        String GetDefaultLanguage();

        void SetDefaultLanguage(String defaultLanguage);

        HashSet<String> GetLanguages();

        void SetLanguages(HashSet<String> languages);
    }
}
