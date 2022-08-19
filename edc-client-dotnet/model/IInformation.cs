namespace edcClientDotnet.model
{
    // This component define the information about the documentation (version, label, ...)
    public interface IInformation
    {
        String DefaultLanguage { get; set; }

        HashSet<String> Languages { get; set; }
    }
}
