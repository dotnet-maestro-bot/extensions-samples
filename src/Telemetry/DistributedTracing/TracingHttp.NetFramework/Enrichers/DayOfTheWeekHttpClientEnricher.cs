// Copyright (c) Microsoft Corporation. All Rights Reserved.

using System.Diagnostics;
using System.Net;
using Microsoft.R9.Extensions.HttpClient.Tracing;
using Microsoft.R9.Extensions.Time;

namespace TracingHttp.NetFramework
{
    internal sealed class DayOfTheWeekHttpClientEnricher : IHttpClientTraceEnricher
    {
        public void Enrich(Activity activity, HttpWebRequest? request, HttpWebResponse? response)
        {
            if (request != null && response != null)
            {
                _ = activity.SetTag("HttpClientEnricher_DayOfWeek", SystemClock.Instance.UtcNow.DayOfWeek);
            }
        }
    }
}