// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TracingHttp;

/// <summary>
/// Extensions for Service metadata.
/// </summary>
internal static class ServiceMetadataExtensions
{
    private const string DefaultSectionName = "clustermetadata:service";

    /// <summary>
    /// Registers configuration provider for Service metadata.
    /// </summary>
    /// <param name="builder">The configuration builder.</param>
    /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment" />.</param>
    /// <param name="sectionName">Section name to save configuration into. Default set to "clustermetadata:service".</param>
    /// <returns>The input configuration builder for call chaining.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> or <paramref name="hostEnvironment"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="sectionName"/> is either <see langword="null"/>, empty or whitespace.</exception>
    public static IConfigurationBuilder AddServiceMetadata(this IConfigurationBuilder builder, IHostEnvironment hostEnvironment, string sectionName = DefaultSectionName)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        _ = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            throw new ArgumentException(default, nameof(sectionName));
        }

        return builder.Add(new ServiceMetadataSource(hostEnvironment, sectionName));
    }
    /// <summary>
    /// Adds an instance of <see cref="ServiceMetadata"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="section">The configuration section to bind.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddServiceMetadata(this IServiceCollection services, IConfigurationSection section)
    {
        _ = services ?? throw new ArgumentNullException(nameof(services));
        _ = section ?? throw new ArgumentNullException(nameof(section));

        //_ = services.AddValidatedOptions<ServiceMetadata, ServiceMetadataValidator>().Bind(section);

        return services;
    }
}
