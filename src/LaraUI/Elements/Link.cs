﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Integrative.Lara
{
    /// <summary>
    /// The 'link' HTML5 element.
    /// </summary>
    /// <seealso cref="Element" />
    public sealed class Link : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link() : base("link")
        {
        }

        /// <summary>
        /// Gets or sets the 'crossorigin' HTML5 attribute.
        /// </summary>
        public string? CrossOrigin
        {
            get => GetAttributeLower("crossorigin");
            set => SetAttributeLower("crossorigin", value);
        }

        /// <summary>
        /// Gets or sets the 'href' HTML5 attribute.
        /// </summary>
        public string? HRef
        {
            get => GetAttributeLower("href");
            set => SetAttributeLower("href", value);
        }

        /// <summary>
        /// Gets or sets the 'hreflang' HTML5 attribute.
        /// </summary>
        public string? HRefLang
        {
            get => GetAttributeLower("hreflang");
            set => SetAttributeLower("hreflang", value);
        }

        /// <summary>
        /// Gets or sets the 'media' HTML5 attribute.
        /// </summary>
        public string? Media
        {
            get => GetAttributeLower("media");
            set => SetAttributeLower("media", value);
        }

        /// <summary>
        /// Gets or sets the 'rel' HTML5 attribute.
        /// </summary>
        public string? Rel
        {
            get => GetAttributeLower("rel");
            set => SetAttributeLower("rel", value);
        }

        /// <summary>
        /// Gets or sets the 'sizes' HTML5 attribute.
        /// </summary>
        public string? Sizes
        {
            get => GetAttributeLower("sizes");
            set => SetAttributeLower("sizes", value);
        }

        /// <summary>
        /// Gets or sets the 'type' HTML5 attribute.
        /// </summary>
        public string? Type
        {
            get => GetAttributeLower("type");
            set => SetAttributeLower("type", value);
        }
    }
}
