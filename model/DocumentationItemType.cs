
namespace edc_client_dotnet.model
{
    internal enum DocumentationItemType
    {
        /// <summary>
        ///     Unknown documentation item type
        /// </summary>
        UNKNOWN,

        /// <summary>
        ///     Chapter documentation type. This is a documentation item which can contain DOCUMENT and CONTEXTUEL documentation item.
        /// </summary>
        CHAPTER,

        /// <summary>
        ///     Contextual documentation item type ie bricks. It can contain ARTICLE documentation type
        /// </summary>
        CONTEXTUAL,

        /// <summary>
        ///     Document documentation item type.It can contain ARTICLE documentation type
        /// </summary>
        DOCUMENT,

        /// <summary>
        ///     Article documentation item type. it can't contain anything.
        /// </summary>
        ARTICLE
    }
}
