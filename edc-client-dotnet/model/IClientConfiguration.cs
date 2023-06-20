namespace edcClientDotnet.model
{
    public interface IClientConfiguration
    {
        /// <summary>
        ///     <para>GET: Return the server url without context.</para>
        ///     SET: Define the server url like http:/localhost:8080/.
        ///     The url is just the protocol, the host and the port.
        /// </summary>
        /// <returns>the server url</returns>
        String ServerUrl { get; set; }
        /// <summary>
        ///     <para>GET: Return the WebHelp context.</para>
        ///     <para>This is the context to use the WebHelp application. The default value is "help".</para>
        ///     <para>SET: Define the WebHelp context.</para>
        /// </summary>
        /// <returns>the WebHelp context</returns>
        /// <exception cref="InvalidUrlException">if the context is null</exception>
        String WebHelpContext { get; set; }

        /// <summary>
        ///     <para>Return the documentation context.</para>
        ///     <para>This is the context to use to read the documentation (edc export). The default value is doc.</para>
        ///     SET: Define the documentation context.
        /// </summary>
        /// <returns>the documentation context</returns>
        String DocumentationContext { get; set; }

        /// <summary>
        ///     Generate the WebHelp url bases on server url and its context.
        /// </summary>
        /// <returns>the WebHelp url</returns>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String WebHelpUrl { get; }

        /// <summary>
        ///     Generate the documentation url based on server url and its context.
        /// </summary>
        /// <returns>the documentation url</returns>
        /// <exception cref="InvalidUrlException">if the url is malformed</exception>
        String DocumentationUrl { get; }
    }
}
