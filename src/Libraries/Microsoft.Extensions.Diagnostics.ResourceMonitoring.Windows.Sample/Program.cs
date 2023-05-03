// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.R9.Extensions.Diagnostics;

namespace Microsoft.Extensions.Diagnostics.ResourceMonitoring.Windows.Sample;

/// <summary>
/// The example shows two different ways of using the ResourceUtilization package, each way is written in a separate static method.
/// </summary>
internal static class Program
{
    internal static readonly TimeProvider Clock = TimeProvider.System;

    public static async Task Main()
    {
        await SampleUsingServicesCollection().ConfigureAwait(false);
    }

    public static async Task SampleUsingServicesCollection()
    {
        using var serviceProvider = new ServiceCollection()
            .AddLogging(x => x.AddConsole())
            .AddResourceUtilization(builder =>
                builder
                    .ConfigureTracker(options =>
                    {
                        options.CollectionWindow = TimeSpan.FromSeconds(5);
                        options.SamplingFrequency = TimeSpan.FromSeconds(1);
                    })
                    .AddWindowsProvider()
                    .AddPublisher<CustomPublisher>())
            .BuildServiceProvider();

        // In the .NET core the hosted service will start automatically.
        // In this example though we are using plain service provider so we need to do it manually.
        // There might be many hosted services so we start all of them.
        var hostedService = serviceProvider.GetRequiredService<IResourceUtilizationTracker>() as IHostedService;
        if (hostedService != null)
        {
            // We start hosted service to publish resource utilization to our console.
            await hostedService.StartAsync(CancellationToken.None).ConfigureAwait(false);

            // We wait 60 seconds to collect the data and see how it works.
            await Clock.Delay(TimeSpan.FromSeconds(60), CancellationToken.None).ConfigureAwait(false);

            // We gracefully shutdown the hosted service.
            await hostedService.StopAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }

    public static async Task SampleWithWindowsPerfCounterPublisher()
    {
        // First, we prepare the Windows performance counters.
        CountersSetup.PreparePerformanceCounters();

        // Then we build a host for our tracker and configure it to use the Windows performance counters.
        await Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => services.AddLogging(x => x.AddConsole()))
            .ConfigureResourceUtilization(builder =>
                builder
                    .ConfigureTracker(options =>
                    {
                        options.CollectionWindow = TimeSpan.FromSeconds(5);
                        options.SamplingFrequency = TimeSpan.FromSeconds(1);
                    })
                    .AddWindowsProvider()
                    .AddWindowsPerfCounterPublisher()
                    .AddPublisher<CustomPublisher>())
            .Build()
            .RunAsync()
            .ConfigureAwait(false);
    }
}
