namespace DocumentsService.Core.Interfaces;

public interface IBlobStorageRepository
{
    Task UploadBlobAsync(string blobName, Stream content);
    Task DeleteBlobAsync(string blobName);
    Task<Stream> DownloadBlobAsync(string blobName);
}