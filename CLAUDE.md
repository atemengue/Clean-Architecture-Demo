# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the solution
dotnet build

# Run the API (HTTP on http://localhost:5110)
dotnet run --project "Clean-Architecture Demo"

# Run with a specific launch profile
dotnet run --project "Clean-Architecture Demo" --launch-profile https

# Run tests (once a test project exists)
dotnet test

# Run a single test by filter
dotnet test --filter "FullyQualifiedName~MethodName"
```

## Architecture

This is a **Clean Architecture** implementation in .NET 10. Dependencies flow strictly inward:

```
Presentation → Application → Domain
```

### Domain (`/Domain`)
The innermost layer — no external dependencies. Contains:
- **Entities:** `Event`, `Category`, `Order`
- **Base class:** `AuditableEntity` (provides `CreatedBy`, `CreatedDate`, `LastModifiedBy`, `LastModifiedDate`)

### Application (`/Application`)
Business use case layer — depends only on Domain. Contains:
- **DTOs:** `/DTOs/Event/` — `EventDto`, `CreateEventDto`, `UpdateEventDto`
- **Services:** `/Services/` — interfaces and implementations (currently stub placeholders for `Categories`, `Events`, `Order`)

New features follow this pattern: define an interface in `/Services/<Feature>/I<Feature>Service.cs`, implement it alongside, and use DTOs in `/DTOs/<Feature>/`.

### Presentation (`/Clean-Architecture Demo`)
ASP.NET Core Web API — depends on Application and Domain. Contains controllers, `Program.cs`, and `appsettings.json`.

OpenAPI/Swagger is configured via `Microsoft.AspNetCore.OpenApi`. The `.http` file at the project root can be used for manual endpoint testing.

## Key Conventions

- Nullable reference types are enabled in all projects (`<Nullable>enable</Nullable>`)
- Implicit usings are enabled (`<ImplicitUsings>enable</ImplicitUsings>`)
- `AuditableEntity` should be the base class for entities that need audit tracking
- Service interfaces belong in the Application layer; implementations are also in Application (no separate Infrastructure layer yet)
