// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TracingHttp;

internal static class Program
{
    public static Task Main(string[] args)
    {
        return CreateHostBuilder(args).Build().RunAsync();
    }

#if NETCOREAPP3_1
   public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureAppConfiguration((hostingContext, builder) => builder.AddServiceMetadata(hostingContext.HostingEnvironment, "ServiceMetadata"));
#else
    public static IWebHostBuilder CreateHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
#endif
}
