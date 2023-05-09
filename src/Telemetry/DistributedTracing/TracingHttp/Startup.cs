// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Builder;
#if NETCOREAPP3_1
#else
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Telemetry;
using Microsoft.Extensions.Compliance.Classification.Simple;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Telemetry.Tracing;
using Microsoft.R9.Extensions.Enrichment;
using OpenTelemetry.Trace;

namespace TracingHttp;

internal sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
#if NETCOREAPP3_1
        _ = services.AddRouting();
        _ = services.AddControllersWithViews();
        _ = services.AddHttpClient();
#else
        _ = services
            .AddMvc()
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
#endif

        _ = services.AddSingleton<ITracingService, TracingService>();

        // Configure redaction. XXHashRedactor is used as an example
        _ = services.AddRedaction(redaction => redaction.SetXXHashRedactor(_ => { }, SimpleClassifications.PrivateData));

        // Adding service metadata information using appsettings.json configuration file:
        _ = services.AddServiceMetadata(Configuration.GetSection("ServiceMetadata"));

        _ = services.AddOpenTelemetry()
            .WithTracing(builder => builder
            .SetSampler(new AlwaysOnSampler())
            .AddSource(nameof(TracingService))

            // Adding service enrichment to traces
            // ApplicationName and EnvironmentName are enabled by default,
            // so enabling BuildVersion and DeploymentRing to ensure traces are enriched with them as well.
            .AddServiceTraceEnricher(options =>
            {
                options.BuildVersion = true;
                options.DeploymentRing = true;
            })

            .AddHttpTracing(options =>
            {
                options.RouteParameterDataClasses.Add("chatId", SimpleClassifications.PrivateData);
                options.ExcludePathStartsWith.Add("home");
            })
            .AddHttpClientTracing(options =>
            {
                options.RouteParameterDataClasses.Add("chatId", SimpleClassifications.PrivateData);
            })
            .AddHttpTraceEnricher<DayOfTheWeekHttpEnricher>()
            .AddHttpClientTraceEnricher<DayOfTheWeekHttpClientEnricher>()
            .AddConsoleExporter()); // Console exporter is for demo purposes. AddGenevaExporter() to be used in real world.
    }

#if NETCOREAPP3_1
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Startup class")]
    public void Configure(IApplicationBuilder app)
    {
        _ = app.UseRouting();

        _ = app.UseEndpoints(endpoints => _ = endpoints.MapDefaultControllerRoute());
    }
#else

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable IDE0060 // Remove unused parameter
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        _ = app.UseEndpointRouting();

        _ = app.UseMvc();
    }
#endif
}
