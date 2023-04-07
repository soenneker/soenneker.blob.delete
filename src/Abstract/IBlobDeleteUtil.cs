using System.Threading.Tasks;
using Azure;

namespace Soenneker.Blob.Delete.Abstract;

/// <summary>
/// A utility library for Azure Blob storage delete operations <para/>
/// Typically Scoped IoC
/// </summary>
public interface IBlobDeleteUtil
{
    ValueTask<Response<bool>> Delete(string containerName, string relativeUrl);

    ValueTask<bool> DeleteContainer(string containerName);

    /// <summary>
    /// Deletes each blob inside a directory
    /// </summary>
    /// <returns>True if all deletes are successful, False if any single one fails</returns>
    ValueTask<bool> DeleteDirectory(string containerName, string directory);
}