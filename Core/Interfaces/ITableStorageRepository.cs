using DocumentsService.Core.Entities;

namespace DocumentsService.Core.Interfaces;

public interface ITableStorageRepository
{
    Task UpsertDocumentAsync(Documents document);
    Task DeleteDocumentAsync(string id);
    Task<Documents> GetDocumentByIdAsync(string id);
    Task<IEnumerable<Documents>> GetAllDocumentsAsync();
    Task<string> GetDocumentUrlAsync(string id);
}