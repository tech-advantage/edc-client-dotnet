namespace edcClientDotnet.model
{
    // This component define the information about the documentation (version, label, ...)
    public interface IInformation
    {
        String GetDefaultLanguage();

        void SetDefaultLanguage(String defaultLanguage);

        HashSet<String> GetLanguages();

        void SetLanguages(HashSet<String> languages);
    }
}
