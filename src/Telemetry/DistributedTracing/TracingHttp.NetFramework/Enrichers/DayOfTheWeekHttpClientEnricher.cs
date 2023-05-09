// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Http.Telemetry.Tracing;

namespace TracingHttp.NetFramework
{
    internal sealed class DayOfTheWeekHttpClientEnricher : IHttpClientTraceEnricher
    {
        public void Enrich(Activity activity, HttpWebRequest? request, HttpWebResponse? response)
        {
            if (request != null && response != null)
            {
                _ = activity.SetTag("HttpClientEnricher_DayOfWeek", TimeProvider.System.GetUtcNow().DayOfWeek);
            }
        }
    }
}
