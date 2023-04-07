using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blob.Client.Registrars;
using Soenneker.Blob.Container.Registrars;
using Soenneker.Blob.Delete.Abstract;
using Soenneker.Blob.Fetch.Registrars;

namespace Soenneker.Blob.Delete.Registrars;

/// <summary>
/// A utility library for Azure Blob storage delete operations
/// </summary>
public static class BlobDeleteUtilRegistrar
{
    /// <summary>
    /// Recommended
    /// </summary>
    public static void AddBlobDeleteUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IBlobDeleteUtil, BlobDeleteUtil>();
        services.AddBlobClientUtilAsSingleton();
        services.AddBlobContainerUtilAsSingleton();
        services.AddBlobFetchUtilAsSingleton();
    }

    public static void AddBlobDeleteUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IBlobDeleteUtil, BlobDeleteUtil>();
        services.AddBlobClientUtilAsSingleton();
        services.AddBlobContainerUtilAsSingleton();
        services.AddBlobFetchUtilAsScoped();
    }
}