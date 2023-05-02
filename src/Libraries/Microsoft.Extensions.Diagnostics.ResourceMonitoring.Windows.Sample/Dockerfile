# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0-windowsservercore-ltsc2019

# Set the working directory of the container.
WORKDIR /app

# Copy published assemblies to the conatiner.
COPY ./bin/Debug/net6.0/publish /app

# Initiate the sample entry point
ENTRYPOINT powershell.exe -File .\RunAndMonitor.ps1
