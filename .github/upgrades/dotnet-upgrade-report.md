# .NET 10 Upgrade Report

## Project target framework modifications

| Project name                                                                                  | Old Target Framework | New Target Framework | Commits                                   |
|:----------------------------------------------------------------------------------------------|:--------------------:|:--------------------:|-------------------------------------------|
| GLeadGenerator.Integration.Contract\\GLeadGenerator.Integration.Contract.csproj |       net7.0        |       net10.0       | 5caa1fc                                    |
| GLeadGenerator.Integration.Service\\GLeadGenerator.Integration.Service.csproj   |       net7.0        |       net10.0       | 27c85b6, 478667a                           |
| GLeadGenerator.Integration.Api\\GLeadGenerator.Integration.Api.csproj           |       net7.0        |       net10.0       | 9c8ed9f, 700cbd4, 2648cab, 0ae6097        |
| GLeadGenerator.Infrastructure\\GLeadGenerator.Infrastructure.csproj             |       net7.0        |       net10.0       | f38674c, 99cf022, 1679407, c5fc3e0        |

## NuGet Packages

| Package Name                                   | Old Version | New Version | Commit Ids                          |
|:-----------------------------------------------|:-----------:|:-----------:|-------------------------------------|
| Microsoft.AspNetCore.OpenApi                   |   7.0.2     |   10.0.1    | b975c63                             |
| Microsoft.Extensions.Http.Polly                |   7.0.2     |   10.0.1    | b975c63, 478667a                    |
| Polly                                          |   7.2.3     |   7.2.4     | e5918e4                             |
| Scalar.AspNetCore                              |            |   2.11.3    | 700cbd4                             |
| FluentValidation.AspNetCore                    |   11.2.2    |            | 99cf022                             |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets |   1.17.0    |            | b975c63                             |

## All commits

| Commit ID | Description                                                                                                                                    |
|:----------|:-----------------------------------------------------------------------------------------------------------------------------------------------|
| c60cc3a   | Commit upgrade plan.                                                                                                                            |
| 77c0fdd   | Update target framework to net10.0 in .csproj file.                                                                                            |
| 5caa1fc   | chore: retarget integration contract to net10 and rename namespaces.                                                                           |
| 27c85b6   | Update target framework in Service.csproj to net10.0.                                                                                          |
| 478667a   | Renamed namespaces/usings to GLeadGenerator and added Microsoft.Extensions.Http.Polly 10.0.1 per plan.                                         |
| afc8d4f   | Adjusted JsonPlaceholderHttpClient namespace imports while reconciling contract DTO references.                                                |
| c106fb0   | Interim removal of JsonPlaceholder contract using directives in JsonPlaceholderService while resolving build errors.                           |
| 6b7f9b1   | Commit changes before fixing errors.                                                                                                           |
| 649907f   | Addressed namespace mismatches between Integration.Service and contract DTOs.                                                                  |
| 3704d8d   | Commit changes before fixing errors.                                                                                                           |
| 94b71ab   | Added contract namespace import so GetUsers DTOs resolve in JsonPlaceholderService.                                                            |
| 30e8b5b   | Pointed JsonPlaceholderService to the Users DTO namespace to fix UserDto resolution.                                                           |
| 959bfa8   | Added contract namespace import for GetUsersRequest/GetUsersResponse in JsonPlaceholderService.                                                |
| 9c8ed9f   | Update target framework to net10.0 in .csproj file for Integration.Api.                                                                        |
| b975c63   | Update package versions in GLeadGenerator.Integration.Api.csproj (OpenAPI/Polly + remove Azure container tools).                        |
| e5918e4   | Update Polly package version in GLeadGenerator.Integration.Api.csproj.                                                                  |
| 700cbd4   | Replaced Swashbuckle setup with Scalar by installing Scalar.AspNetCore, enabling AddOpenApi, and mapping Scalar API reference.                  |
| 2648cab   | Renamed API namespaces and usings from GLeadGenerator to GLeadGenerator, covering Program.cs and the JsonPlaceholder controller.        |
| f38674c   | Update target framework to net10.0 in GLeadGenerator.Infrastructure.csproj.                                                            |
| 99cf022   | Remove FluentValidation.AspNetCore from infrastructure csproj.                                                                                 |
| 1679407   | Removed FluentValidation handlers/interceptors and introduced InvalidModelStateException, DataAnnotationsValidatorInterceptor, and related handler. |
| c5fc3e0   | Renamed all infrastructure namespaces/usings from GLeadGenerator to GLeadGenerator.                                                     |
| 0ae6097   | chore: wire Scalar+DataAnnotations validation in integration API.                                                                              |

## Project feature upgrades

### GLeadGenerator.Integration.Contract
- Retargeted to net10.0 and aligned all namespaces/usings with the new `GLeadGenerator` root to keep DTOs consistent for downstream projects.

### GLeadGenerator.Integration.Service
- Retargeted to net10.0, brought `Microsoft.Extensions.Http.Polly` up to 10.0.1, and made typed HTTP clients resilient on the newer SDK.
- Updated service and HTTP client namespaces/usings to match the `GLeadGenerator` contracts so DTOs resolve correctly. Added the missing contract imports that caused build breaks after the rename.

### GLeadGenerator.Integration.Api
- Retargeted to net10.0, upgraded OpenAPI/Polly dependencies, added Scalar.AspNetCore, and removed Swashbuckle/Azure container tooling.
- Replaced Swagger middleware with `AddOpenApi` + Scalarâ€™s `MapScalarApiReference`, enabling the new API reference UI.
- Introduced the infrastructure `DataAnnotationsValidatorInterceptor`/`InvalidModelStateException` flow, wired the shared error middleware, and renamed all namespaces/usings to `GLeadGenerator`.

### GLeadGenerator.Infrastructure
- Retargeted to net10.0 and added `Microsoft.AspNetCore.App` as a framework reference so ASP.NET abstractions compile cleanly.
- Removed FluentValidation dependencies, replacing them with `InvalidModelStateException`, `DataAnnotationsValidatorInterceptor`, and `DataAnnotationsValidationExceptionHandler` to standardize validation handling.
- Renamed every namespace/using from `GLeadGenerator` to `GLeadGenerator` so consuming projects pick up the new branding.

## Next steps
- `dotnet build GLeadGenerator.sln` currently fails because the solution still references legacy project files that no longer exist on disk (`GLeadGenerator.Api|Contract|Service|Model|Repository|Query|Web`). Update the solution to point at the renamed `GLeadGenerator.*` projects or restore the missing project files before running full solution builds.
- Run the full solution build/test pass once the orphaned solution entries are corrected to validate the net10.0 upgrade end-to-end.

## Token usage and cost
Token accounting data is unavailable in this environment, so per-request token counts and cost information cannot be provided.

