# break on errors
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"
$PSDefaultParameterValues['*:ErrorAction'] = 'Stop'

# Build and publish the project.
Write-Output ">>> Building and publishing the sample project... <<<"
..\..\..\.dotnet\dotnet publish --framework net8.0 /bl
Write-Output "#####################################################`n"

try {
    # Build the docker image.
    Write-Output ">>> Building the docker image... <<<"
    Push-Location $PSScriptRoot/../../../artifacts/bin/Microsoft.Extensions.Diagnostics.ResourceMonitoring.Windows.Sample/Release/net8.0/
    docker image build . -t resource-utilization-net8
    Write-Output "#####################################################`n"
}
finally {
    Pop-Location
}

# Run the container.
Write-Output ">>> Runnig the container... <<<"
docker container run resource-utilization-net8
