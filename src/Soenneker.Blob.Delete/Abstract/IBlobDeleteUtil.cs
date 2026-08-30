using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace Soenneker.Blob.Delete.Abstract;

/// <summary>
/// Deletes Azure blobs, containers, and groups of blobs selected by a name prefix.
/// </summary>
public interface IBlobDeleteUtil
{
    /// <summary>
    /// Deletes a blob if it exists.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="relativeUrl">Blob name within the container.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The Azure response whose value is <c>true</c> when a blob was deleted and <c>false</c> when it was absent.</returns>
    ValueTask<Response<bool>> Delete(string containerName, string relativeUrl, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a container if it exists.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns><c>true</c> when the container was deleted; otherwise, <c>false</c>.</returns>
    ValueTask<bool> DeleteContainer(string containerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes each blob whose name is returned for the supplied virtual-directory prefix.
    /// </summary>
    /// <param name="containerName">Name of the container to target.</param>
    /// <param name="directory">Directory to read from or write to.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns><c>true</c> when every listed blob was deleted or the prefix was empty; otherwise, <c>false</c>.</returns>
    ValueTask<bool> DeleteDirectory(string containerName, string directory, CancellationToken cancellationToken = default);
}
