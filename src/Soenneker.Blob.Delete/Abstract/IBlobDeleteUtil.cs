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
    /// Executes the delete operation.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="relativeUrl">The relative url.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<Response<bool>> Delete(string containerName, string relativeUrl, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes container.
    /// </summary>
    /// <param name="containerName">The container name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<bool> DeleteContainer(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes each blob inside a directory
    /// </summary>
    /// <returns>True if all deletes are successful, False if any single one fails</returns>
    ValueTask<bool> DeleteDirectory(string containerName, string directory, CancellationToken cancellationToken = default);
}