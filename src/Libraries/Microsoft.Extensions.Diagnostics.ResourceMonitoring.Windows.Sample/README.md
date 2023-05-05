# Resource Monitoring

Historically, service applications have needed to inspect the CPU and memory utilization of their hosting environment in order to make intelligent contextual decisions. Obtaining utilization numbers in the context of a container can be challenging. If you're not careful, you can end up with the resource utilization of the host, rather than of the container.

The `Microsoft.Extensions.Diagnostics.ResourceMonitoring` package makes it possible to monitor the utilization of your application or service.

## How it works

The package `Microsoft.Extensions.Diagnostics.ResourceMonitoring` provides a basic implementation of the `IResourceUtilizationTracker` interface which runs a simple timer in the background. It frequently gathers the data you need and then exposes it in two ways:

1. It caches the data in memory for you to use. You can call `GetUtilization` to retrieve the CPU and memory percentages directly.

    > :information: These are multi-instance counters, but there's only one set of information exposed, labeled `(total)`. This counter is accessible only within the container.

1. On Windows, it publishes the data under the two performance counters (which can be viewed in  into Performance Monitor):

    * CPU: `\COSMIC Containers(total)\% CPU Limit Utilization`
    * Mem: `\COSMIC Containers(total)\% Memory Limit Utilization`

    > On Windows, you have to add a reference to the package Microsoft.R9.Extensions.Diagnostics.ResourceUtilization.Windows in order to be able to publish utilization data to Windows performance counters.
