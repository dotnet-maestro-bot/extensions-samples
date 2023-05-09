// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace TracingHttp;

/// <summary>
/// Service Metadata information.
/// </summary>
internal sealed record ServiceMetadata
{
    /// <summary>
    /// Gets or sets a value that represents the deployment ring from which the event is logged.
    /// </summary>
    public string? DeploymentRing { get; set; }

    /// <summary>
    /// Gets or sets a value that represents a build version.
    /// </summary>
    public string? BuildVersion { get; set; }

    /// <summary>
    /// Gets or sets a value that represents the application name.
    /// </summary>
    [Required]
    public string ApplicationName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value that represents the environment name, such as Development, Staging, Production.
    /// </summary>
    [Required]
    public string EnvironmentName { get; set; } = string.Empty;
}
