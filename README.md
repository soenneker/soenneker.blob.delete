[![](https://img.shields.io/nuget/v/Soenneker.Blob.Delete.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Delete/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.delete/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blob.delete/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Blob.Delete.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Blob.Delete/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blob.delete/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blob.delete/actions/workflows/codeql.yml)

# Soenneker.Blob.Delete

Deletes individual Azure blobs, entire containers, or every blob under a virtual-directory prefix.

## Install

```bash
dotnet add package Soenneker.Blob.Delete
```

Register the utility in `Program.cs`:

```csharp
using Soenneker.Blob.Delete.Registrars;
builder.Services.AddBlobDeleteUtilAsSingleton();
```

Scoped registration is also available.

The underlying Blob packages require `Azure:Storage:Blob:ConnectionString` in configuration. Store that value in a secret provider.

## Delete one blob

```csharp
Response<bool> response = await blobDelete.Delete(
    "invoices",
    "2026/obsolete.pdf",
    cancellationToken);

bool deleted = response.Value;
```

`false` means the blob did not exist. Azure authorization, lease, snapshot, and service failures throw `RequestFailedException`. The default Azure delete behavior does not include snapshots, so a blob with snapshots may require explicit snapshot cleanup before this method can delete it.

## Delete a virtual directory

Azure Blob Storage has blob-name prefixes rather than physical directories. This method lists the supplied prefix and deletes each returned blob sequentially:

```csharp
bool allDeleted = await blobDelete.DeleteDirectory(
    "exports",
    "temporary/2026/",
    cancellationToken);
```

The result is `true` when every listed blob was deleted or no blobs matched. It is `false` if a listed blob disappeared before its delete completed. Azure request failures are not converted to `false`; they stop the operation and propagate.

Prefix matching can include more blobs than intended when the trailing delimiter is omitted. Use `temporary/2026/`, not `temporary/2026`, when only that virtual directory should be removed.

## Delete a container

```csharp
bool deleted = await blobDelete.DeleteContainer(
    "temporary-exports",
    cancellationToken);
```

Container deletion removes all blobs, versions, and snapshots in that container. Treat the container name as trusted application configuration, not direct user input.

## Important behavior

- The shared client/container utilities normalize container names to lowercase.
- Those utilities ensure the target container exists while resolving clients. As a result, deleting a missing blob or container can briefly create the missing container before deletion. Avoid using these methods as existence probes.
- Directory deletion is not transactional. Cancellation or a failure can leave a partially deleted prefix.
- Soft delete, versioning, immutability policies, legal holds, and leases can change whether data is recoverable or whether deletion is allowed.

- Cancellation stops pending work; it does not undo work that has already completed.
