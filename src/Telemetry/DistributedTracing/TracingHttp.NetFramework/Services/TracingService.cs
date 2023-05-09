// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Telemetry.Http;
using Microsoft.R9.Extensions.Telemetry;

namespace TracingHttp.NetFramework
{
    public class TracingService : ITracingService
    {
        private static readonly ActivitySource _activitySource = new(nameof(TracingService));

        /// <summary>
        /// This demonstrates how to add custom tags.
        /// </summary>
        /// <returns>Added custom tags.</returns>
        public IEnumerable<KeyValuePair<string, string?>> GetTracingServiceTags()
        {
            using var activity = _activitySource.StartActivity("ActivityName");
            activity?.SetTag(nameof(TracingService), TimeProvider.System.GetUtcNow().ToString(CultureInfo.InvariantCulture));
            return activity?.Tags ?? Enumerable.Empty<KeyValuePair<string, string?>>();
        }

        public async Task<HttpStatusCode> GetChatById(string chatId)
        {
            var request = WebRequest.CreateHttp($"https://localhost:5001/chats/details/{chatId}");
            request.SetRequestMetadata(new RequestMetadata
            {
                RequestRoute = "/chats/details/{chatId}",
            });

            try
            {
                var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);

                return response.StatusCode;
            }
            catch (WebException e)
            {
                return ((HttpWebResponse)e.Response).StatusCode;
            }
        }
    }
}
