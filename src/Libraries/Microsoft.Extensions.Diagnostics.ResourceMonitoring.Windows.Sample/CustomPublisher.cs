// Copyright (c) Microsoft Corporation. All Rights Reserved.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.R9.Extensions.Diagnostics;
using Microsoft.R9.Extensions.Time;

namespace ResourceUtilization
{
    /// <summary>
    /// This class is getting the data from the utilization publisher.
    /// </summary>
    /// <remarks>
    /// You can do with this data whatever you want. Publish it to remote service, logs...
    /// In this example we are simply printing it to the console.
    /// </remarks>
    internal class CustomPublisher : IResourceUtilizationPublisher
    {
        internal readonly IClock Clock = SystemClock.Instance;

        public ValueTask PublishAsync(Utilization utilization, CancellationToken cancellationToken)
        {
            Console.Out.WriteLine("- - - Dynamic resource utilization of your machine - - -");
            Console.Out.WriteLine($"[{Clock.UtcNow}]:");
            Console.Out.WriteLine($"\t\t CPU: {utilization.CpuUsedPercentage}.");
            Console.Out.WriteLine($"\t\t Used Memory: {utilization.MemoryUsedPercentage}.");
            Console.Out.WriteLine($"\t\t Used Memory in bytes: {utilization.MemoryUsedInBytes}.");

            Console.Out.WriteLine("\n\n - - - Static resources of your machine: - - -");
            Console.Out.WriteLine($"\t\t Total CPU units: {utilization.SystemResources.GuaranteedCpuUnits}");
            Console.Out.WriteLine($"\t\t Total Memory in bytes: {utilization.SystemResources.GuaranteedMemoryInBytes}");

            return default;
        }
    }
}