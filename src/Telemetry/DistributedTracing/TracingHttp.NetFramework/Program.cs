// Copyright (c) Microsoft Corporation. All Rights Reserved.

using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TracingHttp.NetFramework
{
    internal static class Program
    {
        public static Task Main(string[] args)
        {
            return CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}