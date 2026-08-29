[![](https://img.shields.io/nuget/v/Soenneker.Blob.Delete.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Delete/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.delete/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blob.delete/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blob.Delete.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Delete/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.delete/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blob.delete/actions/workflows/codeql.yml)

# Soenneker.Blob.Delete

A utility library for Azure Blob storage delete operations Typically Scoped IoC.

## Install

```bash
dotnet add package Soenneker.Blob.Delete
```

## Quick start

```csharp
using Soenneker.Blob.Delete.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddBlobDeleteUtilAsSingleton();
```

Registers Blob Delete Util with a singleton lifetime.

## What you get

- `IBlobDeleteUtil` — A utility library for Azure Blob storage delete operations Typically Scoped IoC.
- `BlobDeleteUtilRegistrar` — A utility library for Azure Blob storage delete operations.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IBlobDeleteUtil.Delete(containerName, relativeUrl, cancellationToken)` | Removes the entry associated with the specified key. | A task whose result is the requested response. |
| `IBlobDeleteUtil.DeleteDirectory(containerName, directory, cancellationToken)` | Deletes each blob inside a directory. | True if all deletes are successful, False if any single one fails. |
| `BlobDeleteUtilRegistrar.AddBlobDeleteUtilAsSingleton(services)` | Registers Blob Delete Util with a singleton lifetime. | The same service collection, so additional registrations can be chained. |
| `BlobDeleteUtilRegistrar.AddBlobDeleteUtilAsScoped(services)` | Registers Blob Delete Util with a scoped lifetime. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
