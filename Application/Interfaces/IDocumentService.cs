using DocumentsService.Core.Entities;

namespace DocumentsService.Application.Interfaces;

public interface IDocumentService
{
    Task<string> UploadDocumentAsync(string documentName, Stream content);
    Task DeleteDocumentAsync(string documentId);
    Task<Documents> GetDocumentByIdAsync(string documentId);
    Task<IEnumerable<Documents>> GetAllDocumentsAsync();
    Task<Stream> DownloadDocumentAsync(string documentId);
}