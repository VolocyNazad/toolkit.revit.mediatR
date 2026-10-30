# Revit.MediatR

[![Revit 2021-2027](https://img.shields.io/badge/Revit-2021%20--%202027-green.svg)](https://autodesk.com/revit)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> MediatR pipeline service implementations for the Revit API.

Revit.MediatR is a set of pipeline-pattern service implementations for the Revit API with support
for DI containerization: commands and handlers that know how to marshal execution onto the Revit
API thread via `ExternalEvent` ([VolocyNazad.Revit.Events](https://github.com/VolocyNazad/toolkit.revit.events))
and wrap it in a named transaction — without having to hand-write an `IExternalEventHandler` and
`Transaction` at every call site.

## Installation

```
dotnet add package VolocyNazad.Revit.MediatR
```

The package version is tied to the Revit year: `{RevitYear}.0.{patch}` (e.g. `2025.0.7`). It is
built for a specific Revit version depending on the selected build configuration — the same way as
`Nice3point.Revit.Api.RevitAPI` / `Nice3point.Revit.Api.RevitAPIUI` (floating `$(MajorRevitVersion).*`), which the project depends on.

## DI Registration

```csharp
using Revit.MediatR.DI;

services.AddMediatR();
```

This call registers `VolocyNazad.Revit.Events` (`AddEvents`) and `VolocyNazad.Revit.Context`
(`AddRevitContext`), then MediatR itself along with the handlers for the built-in lambda commands
and the Revit-specific pipeline behaviours (`RevitTransactionBehaviour`,
`RevitExternalEventBehaviour`), which automatically open a transaction and/or marshal execution
onto the Revit API thread.

## Built-in Commands

| Command | Transaction | ExternalEvent |
|---|---|---|
| `LambdaTransactionCommand` | yes | no |
| `LambdaExternalEventCommand` | no | yes |
| `LambdaExternalEventTransactionCommand` | yes | yes |

All of them run in the context of a document (either explicitly passed or the active one from
`IRevitContext`) and return `Response<bool>` — a wrapper carrying the result, an error flag, a
message, and an exception.

### Example: transaction on the active document

```csharp
Response<bool> response = await mediator.Send(
    new LambdaTransactionCommand(
        document: null!, // ActiveDocument is taken from IRevitContext
        transactionName: "Move wall",
        @delegate: (doc, ct) =>
        {
            // changes to the model
            return Task.FromResult(true);
        }));
```

### Example: execution via ExternalEvent (e.g. from a WPF modal window)

```csharp
Response<bool> response = await mediator.Send(
    new LambdaExternalEventTransactionCommand(
        document: activeDocument,
        transactionName: "Create level",
        @delegate: (doc, ct) =>
        {
            // code that must run on the Revit API thread, inside a transaction
            return Task.FromResult(true);
        }));
```

### Custom commands

You can create your own commands by inheriting from `Revit.MediatR.Base.DocumentCommand` and
implementing the marker interfaces `Revit.MediatR.Abstractions.Models.ITransactionCommand` and/or
`IExternalEventCommand`, as well as writing an `IRequestHandler<TCommand, Response<bool>>` for
them — the pipeline behaviours will pick them up automatically via these interfaces.

## Supported Revit Versions

The package is built for every Revit version listed in the `Configurations` element of
`src/Revit.MediatR/Revit.MediatR.csproj` (2021.1.100, 2022.1.80, 2023.1.90, 2024.3.60, 2025.4.60, 2026.4.10, and 2027.3.0), targeting net48, net8.0-windows, or net10.0-windows
depending on the year — the switching and generation of define constants (`IS2025`, `BEFORE2021`,
`AFTER2021`, etc.) is handled by the shared MSBuild SDK
[VolocyNazad.Revit.Sdk](https://github.com/VolocyNazad/toolkit.revit.sdk).

## Development

- `dotnet build --configuration "Release_2026.4.10"` — build for a specific Revit version.
- `dotnet test --configuration "Release_2026.4.10"` — unit tests (`tests/Revit.MediatR.Tests`).
  Tests that need real Revit-typed objects (`Document`) to work are marked `Skip` — the
  `RevitAPI`/`RevitAPIUI` assemblies are not loaded outside of the Revit process.
- The package version is auto-versioned via [MinVer](https://github.com/adamralph/minver) from
  git tags of the form `0.0.7`; to release a new version, create a tag and push it:
  `git tag 0.0.7 && git push origin main --follow-tags`.

## License

[MIT](LICENSE.md)

## Development documentation

- [Development policy](docs/policies/development.md)
- [Repository guide and technology stack](docs/repository.md)
- [Branching policy](docs/policies/branching.md)
- [Commit message policy](docs/policies/commits.md)
- [Changelog policy](docs/policies/changelog.md)
- [Testing policy](docs/policies/testing.md)
- [Benchmark policy](docs/policies/benchmarks.md)

## Contributing
 [CONTRIBUTING.md](CONTRIBUTING.md) before submitting changes.
