﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Integrative.Lara.Main
{
    static class GlobalConstants
    {
        public const string CookieSessionId = "_Lara_SessionId";
        public const string GuidFormat = "N";

#if DEBUG
        public const string LibraryAddress = "/LaraUI-v{0}-debug.js";
#else
        public const string LibraryAddress = "/LaraUI-v{0}.js";
#endif
    }
}