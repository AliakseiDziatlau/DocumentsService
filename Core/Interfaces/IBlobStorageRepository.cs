namespace DocumentsService.Core.Interfaces;

public interface IBlobStorageRepository
{
    Task UploadBlobAsync(string id, Stream content);
    Task DeleteBlobAsync(string id);
    Task<Stream> DownloadBlobAsync(string id);
}