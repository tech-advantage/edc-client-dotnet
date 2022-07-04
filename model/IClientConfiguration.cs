
namespace edc_client_dotnet.model
{
    internal interface IClientConfiguration
    {

        /// <summary>
        ///     Return the server url without context.
        /// </summary>
        /// <returns>the server url</returns>
        String GetServerUrl();

        /// <summary>
        ///     Define the server url like http:/localhost:8080/.
        ///     The url is just the protocol, the host and the port.
        /// </summary>
        /// <param name="serverUrl">serverUrl the server url to set</param>
        void SetServerUrl(String serverUrl);

        /// <summary>
        ///     Return the WebHelp context.
        ///     <p>
        ///     This is the context to use the WebHelp application.
        ///     The default value is "help".
        /// </summary>
        /// <returns>the WebHelp context</returns>
        String GetWebHelpContext();

        /// <summary>
        ///     Define the WebHelp context.
        /// </summary>
        /// <param name="webHelpContext">webHelpContext the WebHelp context to set.</param>
        void SetWebHelpContext(String webHelpContext);

        /// <summary>
        ///     Return the documentation context.
        ///     <p>
        ///     This is the context to use to read the documentation (edc export).
        ///     The default value is doc.
        /// </summary>
        /// <returns>the documentation context</returns>
        String GetDocumentationContext();

        /// <summary>
        ///     Define the documentation context.
        /// </summary>
        /// <param name="documentationContext">documentationContext the documentation context to set</param>
        void SetDocumentationContext(String documentationContext);

        /// <summary>
        ///     Generate the WebHelp url bases on server url and its context.
        /// </summary>
        /// <returns>the WebHelp url</returns>
        String GetWebHelpUrl();

        /// <summary>
        ///     Generate the documentation url based on server url and its context.
        /// </summary>
        /// <returns>the documentation url</returns>
        String GetDocumentationUrl();
    }
}
