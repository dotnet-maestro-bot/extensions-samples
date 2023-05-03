# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0-preview-windowsservercore-ltsc2022

# Set the working directory of the container.
WORKDIR /app

# Copy published assemblies to the conatiner.
COPY ./publish /app

# Initiate the sample entry point
ENTRYPOINT powershell.exe -File ./RunAndMonitor.ps1
