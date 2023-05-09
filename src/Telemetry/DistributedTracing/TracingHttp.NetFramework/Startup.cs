// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TracingHttp.NetFramework;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1822 // Member 'Configure' does not access instance data and can be marked as static

internal class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .AddMvc()
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

        //_ = services.AddSingleton<ITracingService, TracingService>();

        //// Configure redaction. XXHashRedactor is used as an example
        //_ = services.AddRedaction(redaction => redaction.SetXXHashRedactor(_ => { }, SimpleClassifications.PrivateData));

        //_ = services.AddOpenTelemetry()
        //    .WithTracing(builder => builder
        //    .AddSource(nameof(TracingService))
        //    .AddHttpTracing(options =>
        //    {
        //        options.RouteParameterDataClasses.Add("chatId", SimpleClassifications.PrivateData);
        //        options.ExcludePathStartsWith.Add("home");
        //    })
        //    .AddHttpClientTracing(options =>
        //    {
        //        options.RouteParameterDataClasses.Add("chatId", SimpleClassifications.PrivateData);
        //    })
        //    .AddHttpTraceEnricher<DayOfTheWeekHttpEnricher>()
        //    .AddHttpClientTraceEnricher<DayOfTheWeekHttpClientEnricher>()
        //    .AddConsoleExporter()); // Console exporter is for demo purposes. AddGenevaExporter() to be used in real world.
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    [Obsolete("IHostingEnvironment is obsolete in ASP.NET Core 3.0+ only. This sample is for ASP.NET Core 2.x")]
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        _ = app.UseEndpointRouting();

        _ = app.UseMvc();
    }
}
