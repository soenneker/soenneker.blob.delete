using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blob.Client.Registrars;
using Soenneker.Blob.Delete.Abstract;
using Soenneker.Blob.Fetch.Registrars;

namespace Soenneker.Blob.Delete.Registrars;

/// <summary>
/// A utility library for Azure Blob storage delete operations
/// </summary>
public static class BlobDeleteUtilRegistrar
{
    /// <summary>
    /// Adds blob delete util as singleton.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddBlobDeleteUtilAsSingleton(this IServiceCollection services)
    {
        services.AddBlobClientUtilAsSingleton().AddBlobFetchUtilAsSingleton().TryAddSingleton<IBlobDeleteUtil, BlobDeleteUtil>();

        return services;
    }

    /// <summary>
    /// Adds blob delete util as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddBlobDeleteUtilAsScoped(this IServiceCollection services)
    {
        services.AddBlobClientUtilAsSingleton().AddBlobFetchUtilAsScoped().TryAddScoped<IBlobDeleteUtil, BlobDeleteUtil>();

        return services;
    }
}