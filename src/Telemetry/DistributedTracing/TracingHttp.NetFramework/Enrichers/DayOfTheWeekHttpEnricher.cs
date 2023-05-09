// Copyright (c) Microsoft Corporation. All Rights Reserved.

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.R9.Extensions.Time;
using Microsoft.R9.Extensions.Tracing.Http;

namespace TracingHttp.NetFramework
{
    internal sealed class DayOfTheWeekHttpEnricher : IHttpTraceEnricher
    {
        public void Enrich(Activity activity, HttpRequest? request)
        {
            if (request != null)
            {
                _ = activity.SetTag("HttpEnricher_DayOfWeek", SystemClock.Instance.UtcNow.DayOfWeek);
            }
        }
    }
}