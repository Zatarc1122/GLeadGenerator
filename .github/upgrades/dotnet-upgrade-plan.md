# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade BitMouse.LeadGenerator.Integration.Contract\BitMouse.LeadGenerator.Integration.Contract.csproj.
4. Upgrade BitMouse.LeadGenerator.Integration.Service\BitMouse.LeadGenerator.Integration.Service.csproj.
5. Upgrade BitMouse.LeadGenerator.Integration.Api\BitMouse.LeadGenerator.Integration.Api.csproj.
6. Upgrade BitMouse.LeadGenerator.Infrastructure\BitMouse.LeadGenerator.Infrastructure.csproj.

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| _None_                                         | No projects are excluded    |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                   | Current Version | New Version | Description                                                   |
|:-----------------------------------------------|:---------------:|:-----------:|:--------------------------------------------------------------|
| FluentValidation.AspNetCore                    | 11.2.2          |             | Remove FluentValidation pipeline in favor of DataAnnotations. |
| Microsoft.AspNetCore.OpenApi                   | 7.0.2           | 10.0.1      | Required version for .NET 10 + Scalar-based API reference.    |
| Microsoft.Extensions.Http.Polly                | 7.0.2           | 10.0.1      | Align typed HTTP client resilience policies with .NET 10.     |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.17.0          |             | Remove unsupported tooling package for .NET 10 builds.        |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### BitMouse.LeadGenerator.Integration.Contract\BitMouse.LeadGenerator.Integration.Contract.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`.

Feature upgrades:
  - Rename namespaces and using directives from `BitMouse.LeadGenerator` to `GLeadGenerator` across all contract DTOs.

Other changes:
  - Rebuild to confirm DTO API surface remains compatible with consuming services.

#### BitMouse.LeadGenerator.Integration.Service\BitMouse.LeadGenerator.Integration.Service.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`.

NuGet packages changes:
  - Microsoft.Extensions.Http.Polly should be updated from `7.0.2` to `10.0.1` to keep resilient HTTP clients supported on .NET 10.

Feature upgrades:
  - Rename namespaces/usings from `BitMouse.LeadGenerator` to `GLeadGenerator` across background services and handlers.

Other changes:
  - Rebuild service to catch API changes introduced by .NET 10.

#### BitMouse.LeadGenerator.Integration.Api\BitMouse.LeadGenerator.Integration.Api.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`.

NuGet packages changes:
  - Microsoft.AspNetCore.OpenApi should be updated from `7.0.2` to `10.0.1`.
  - Microsoft.Extensions.Http.Polly should be updated from `7.0.2` to `10.0.1`.
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets should be removed because it is not supported on .NET 10 and the project uses Dockerfiles/docker-compose directly.

Feature upgrades:
  - Replace Swashbuckle (AddSwaggerGen/UseSwagger/UseSwaggerUI) with Scalar (AddScalarApiReference/MapScalarApiReference) and remove Swagger-specific packages and middleware.
  - Rename namespaces/usings from `BitMouse.LeadGenerator` to `GLeadGenerator` throughout the API (controllers, middleware, Program.cs).
  - Remove FluentValidation usage (packages, validators, interceptors) and rely on the existing DataAnnotations validation + `InvalidModelStateException`/`DataAnnotationsValidationExceptionHandler` flow only.

Other changes:
  - Rebuild and retest API endpoints (e.g., `JsonPlaceholderController`) against .NET 10 SDK to catch breaking changes.

#### BitMouse.LeadGenerator.Infrastructure\BitMouse.LeadGenerator.Infrastructure.csproj modifications

Project properties changes:
  - Target framework should be changed from `net7.0` to `net10.0`.

NuGet packages changes:
  - FluentValidation.AspNetCore should be removed to eliminate FluentValidation dependencies and interceptors.

Feature upgrades:
  - Ensure DataAnnotations-based validation pipeline is the only validation path by wiring `DataAnnotationsValidatorInterceptor`/`InvalidModelStateException` and removing FluentValidation validators/registrations.
  - Rename namespaces/usings from `BitMouse.LeadGenerator` to `GLeadGenerator` throughout middleware, handlers, and DI modules.

Other changes:
  - Update Serilog, HTTP client, and other infrastructure dependencies to their .NET 10 compatible versions during rebuild and clean up unused dependencies.
