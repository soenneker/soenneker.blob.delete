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
    public static void AddBlobDeleteUtilAsScoped(this IServiceCollection services)
    {
        services.AddBlobClientUtilAsSingleton();
        services.AddBlobFetchUtilAsScoped();
        services.TryAddScoped<IBlobDeleteUtil, BlobDeleteUtil>();
    }
}