using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Soenneker.Blob.Client.Abstract;
using Soenneker.Blob.Container.Abstract;
using Soenneker.Blob.Delete.Abstract;
using Soenneker.Blob.Fetch.Abstract;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Blob.Delete;

///<inheritdoc cref="IBlobDeleteUtil"/>
public class BlobDeleteUtil: IBlobDeleteUtil
{
    private readonly IBlobClientUtil _blobClientUtil;
    private readonly IBlobContainerUtil _blobContainerUtil;
    private readonly IBlobFetchUtil _blobFetchUtil;
    private readonly ILogger<BlobDeleteUtil> _logger;

    public BlobDeleteUtil(IBlobClientUtil blobClientUtil, IBlobContainerUtil blobContainerUtil,
        IBlobFetchUtil blobFetchUtil, ILogger<BlobDeleteUtil> logger)
    {
        _blobClientUtil = blobClientUtil;
        _blobContainerUtil = blobContainerUtil;
        _blobFetchUtil = blobFetchUtil;
        _logger = logger;
    }

    public async ValueTask<Response<bool>> Delete(string containerName, string relativeUrl) 
    {
        _logger.LogDebug("Beginning deletion of {url}", relativeUrl);

        BlobClient blobClient = await _blobClientUtil.Get(containerName, relativeUrl).NoSync();

        Response<bool> response = await blobClient.DeleteIfExistsAsync().NoSync();
        // TODO: Handle this response

        _logger.LogDebug("Finished deletion of {url}", relativeUrl);

        return response;
    }

    public async ValueTask<bool> DeleteContainer(string containerName) 
    {
        _logger.LogDebug("Beginning deletion of container {name}", containerName);
            
        BlobContainerClient containerClient = await _blobContainerUtil.Get(containerName).NoSync();
        Response<bool> response = await containerClient.DeleteIfExistsAsync().NoSync();

        _logger.LogDebug("Finished deletion of container {name}", containerName);

        return response;
    }
        
    public async ValueTask<bool> DeleteDirectory(string containerName, string directory) 
    {
        _logger.LogDebug("Beginning deletion of directory {directory}", directory);

        List<BlobItem> blobs = await _blobFetchUtil.GetAllBlobItems(containerName, directory).NoSync();

        List<bool> deleteStatusList = [];

        foreach (BlobItem? blob in blobs)
        {
            Response<bool> response = await Delete(containerName, blob.Name).NoSync();

            if (!response.Value)
                deleteStatusList.Add(false);
        }

        _logger.LogDebug("Finished deletion of directory {directory}", directory);

        // if any were unsuccessful we return false for this whole thing
        return !deleteStatusList.Any();
    }
}