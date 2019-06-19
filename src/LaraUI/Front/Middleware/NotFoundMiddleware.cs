﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Integrative.Lara.Middleware;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Integrative.Lara
{
    /// <summary>
    /// A middleware to show a simple 'not found' page
    /// </summary>
    public class NotFoundMiddleware
    {
#pragma warning disable IDE0060 // Remove unused parameter: required by ASP.NET Core
        public NotFoundMiddleware(RequestDelegate next)
#pragma warning restore IDE0060 // Remove unused parameter
        {
        }


        /// <summary>
        /// Invokes this middleware
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            await MiddlewareCommon.SendStatusReply(context, HttpStatusCode.NotFound, "HTTP 404: Not found.");
        }
    }
}
