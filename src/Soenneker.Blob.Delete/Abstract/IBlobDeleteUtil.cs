using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace Soenneker.Blob.Delete.Abstract;

/// <summary>
/// A utility library for Azure Blob storage delete operations <para/>
/// Typically Scoped IoC
/// </summary>
public interface IBlobDeleteUtil
{
    /// <summary>
    /// Removes the entry associated with the specified key.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="relativeUrl">URL of the relative to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested response.</returns>
    ValueTask<Response<bool>> Delete(string containerName, string relativeUrl, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes container.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if deletes container; otherwise, false.</returns>
    ValueTask<bool> DeleteContainer(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes each blob inside a directory
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>True if all deletes are successful, False if any single one fails</returns>
    ValueTask<bool> DeleteDirectory(string containerName, string directory, CancellationToken cancellationToken = default);
}
