﻿using edcClientDotnet.factory.model;
using edcClientDotnet.internalImpl.model;
using edcClientDotnet.model;

namespace edcClientDotnet.factory.impl
{
    public class DocumentationItemFactory : IDocumentationItemFactory
    {
        public IDocumentationItem Create()
        {
            return new DocumentationItemImpl();
        }
    }
}
