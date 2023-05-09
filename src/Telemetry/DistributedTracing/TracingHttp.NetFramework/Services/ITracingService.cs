// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TracingHttp.NetFramework
{
    public interface ITracingService
    {
#pragma warning disable S4049 // Properties should be preferred
        IEnumerable<KeyValuePair<string, string?>> GetTracingServiceTags();
#pragma warning restore S4049 // Properties should be preferred

        Task<HttpStatusCode> GetChatById(string chatId);
    }
}