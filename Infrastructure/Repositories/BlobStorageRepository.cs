using Azure.Storage.Blobs;
using DocumentsService.Core.Interfaces;

namespace DocumentsService.Infrastructure.Repositories;

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly BlobContainerClient _containerClient;
    
    public BlobStorageRepository(string connectionString, string containerName)
    {
        // Создаем клиент для контейнера в Blob Storage
        var blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Убеждаемся, что контейнер существует
        _containerClient.CreateIfNotExists();
    }
    
    public async Task UploadBlobAsync(string id, Stream content)
    {
        var blobClient = _containerClient.GetBlobClient(id);
        await blobClient.UploadAsync(content, overwrite: true);
    }

    public async Task DeleteBlobAsync(string id)
    {
        var blobClient = _containerClient.GetBlobClient(id);
        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<Stream> DownloadBlobAsync(string id)
    {
        var blobClient = _containerClient.GetBlobClient(id);
        var downloadResponse = await blobClient.DownloadAsync();
        return downloadResponse.Value.Content;
    }
}