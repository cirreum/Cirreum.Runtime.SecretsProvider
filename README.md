# Cirreum.Runtime.SecretsProvider

[![NuGet Version](https://img.shields.io/nuget/v/Cirreum.Runtime.SecretsProvider.svg?style=flat-square&labelColor=1F1F1F&color=003D8F)](https://www.nuget.org/packages/Cirreum.Runtime.SecretsProvider/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Cirreum.Runtime.SecretsProvider.svg?style=flat-square&labelColor=1F1F1F&color=003D8F)](https://www.nuget.org/packages/Cirreum.Runtime.SecretsProvider/)
[![GitHub Release](https://img.shields.io/github/v/release/cirreum/Cirreum.Runtime.SecretsProvider?style=flat-square&labelColor=1F1F1F&color=FF3B2E)](https://github.com/cirreum/Cirreum.Runtime.SecretsProvider/releases)
[![License](https://img.shields.io/badge/license-MIT-F2F2F2?style=flat-square&labelColor=1F1F1F)](https://github.com/cirreum/Cirreum.Runtime.SecretsProvider/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-003D8F?style=flat-square&labelColor=1F1F1F)](https://dotnet.microsoft.com/)

**Secure secrets management for the Cirreum Runtime Server**

## Overview

**Cirreum.Runtime.SecretsProvider** provides a standardized way to integrate secrets management into the Cirreum Runtime Server. It offers a flexible provider model that supports multiple secret store implementations through a unified configuration interface.

## Installation

Install the package via NuGet:

```bash
dotnet add package Cirreum.Runtime.SecretsProvider
```

## Usage

Register the secrets provider in your host application:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Register your specific secrets provider implementation
builder.RegisterSecretsProvider<MySecretsRegistrar, MySecretsSettings, MyInstanceSettings>();

var app = builder.Build();
```

### Configuration

Configure your secrets provider in `appsettings.json`:

```json
{
  "Cirreum": {
    "YourProviderType": {
      "Providers": {
        "YourProviderName": {
          "Instances": [
            {
              // Your instance-specific settings
            }
          ]
        }
      }
    }
  }
}
```

### Key Features

- **Type-safe configuration** with generic constraints
- **Multiple provider support** with instance-based configuration
- **Duplicate registration prevention** using marker types
- **Integrated deferred logging** for troubleshooting
- **Seamless integration** with Microsoft.Extensions.Hosting

## Contribution Guidelines

1. **Be conservative with new abstractions**  
   The API surface must remain stable and meaningful.

2. **Limit dependency expansion**  
   Only add foundational, version-stable dependencies.

3. **Favor additive, non-breaking changes**  
   Breaking changes ripple through the entire ecosystem.

4. **Include thorough unit tests**  
   All primitives and patterns should be independently testable.

5. **Document architectural decisions**  
   Context and reasoning should be clear for future maintainers.

6. **Follow .NET conventions**  
   Use established patterns from Microsoft.Extensions.* libraries.

## Versioning

{REPO-NAME} follows [Semantic Versioning](https://semver.org/):

- **Major** - Breaking API changes
- **Minor** - New features, backward compatible
- **Patch** - Bug fixes, backward compatible

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Cirreum Foundation Framework**  
*Layered simplicity for modern .NET*