# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build the project
dotnet build

# Build in Release mode
dotnet build -c Release

# Clean build artifacts
dotnet clean

# Restore dependencies
dotnet restore

# Create NuGet package
dotnet pack -c Release

# Run tests (if test projects are added)
dotnet test
```

## Architecture Overview

**Cirreum.Runtime.SecretsProvider** is a .NET 10.0 library that provides secrets management functionality for the Cirreum Runtime Server. This is part of the Cirreum Foundation Framework ecosystem.

### Key Components

1. **HostApplicationBuilderExtensions** (src/Cirreum.Runtime.SecretsProvider/Extensions/Hosting/HostApplicationBuilderExtensions.cs:10)
   - Primary extension method: `RegisterSecretsProvider<TRegistrar, TSettings, TInstanceSettings>`
   - Integrates with Microsoft.Extensions.Hosting for seamless dependency injection
   - Supports registration of multiple provider instances
   - Uses deferred logging for registration process tracking
   - Prevents duplicate registrations using marker types

### Dependencies

- **Microsoft.AspNetCore.App** - Framework reference for ASP.NET Core integration
- **Cirreum.Logging.Deferred** (v1.0.102) - Provides deferred logging capabilities
- **Cirreum.SecretsProvider** (v1.0.0) - Core secrets provider abstractions and implementations

### Configuration Structure

The library expects configuration in the following format:
```
Cirreum:
  {ProviderType}:
    Providers:
      {ProviderName}:
        Instances:
          - {instance settings}
```

### Development Environment

- **Target Framework**: .NET 10.0
- **Language Version**: Latest C#
- **Nullable Reference Types**: Enabled
- **Implicit Usings**: Enabled
- **Documentation**: XML documentation is generated

### Versioning

For local development releases:
- Version: 1.0.100-rc (when building in Release mode locally)
- CI/CD builds use versioning from the build pipeline

The project follows the Cirreum Foundation Framework standards with strict contribution guidelines focused on stability, minimal dependencies, and backward compatibility.