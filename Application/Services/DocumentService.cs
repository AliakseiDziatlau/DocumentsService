using DocumentsService.Application.Interfaces;
using DocumentsService.Core.Entities;
using DocumentsService.Core.Interfaces;

namespace DocumentsService.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly ITableStorageRepository _tableStorageRepository;

    public DocumentService(IBlobStorageRepository blobStorageRepository, ITableStorageRepository tableStorageRepository)
    {
        _blobStorageRepository = blobStorageRepository;
        _tableStorageRepository = tableStorageRepository;
    }
    
    public async Task<string> UploadDocumentAsync(string documentName, Stream content)
    {
        string documentId = Guid.NewGuid().ToString();
        await _blobStorageRepository.UploadBlobAsync(documentId, content);
        var document = new Documents
        {
            Id = documentId,
            Url = GenerateFileUrl(documentId)
        };
        await _tableStorageRepository.UpsertDocumentAsync(document);

        return documentId;
    }

    public async Task DeleteDocumentAsync(string documentId)
    {
        var document = await _tableStorageRepository.GetDocumentByIdAsync(documentId);
        if (document == null)
        {
            throw new KeyNotFoundException("Document is not found");
        }
        
        await _blobStorageRepository.DeleteBlobAsync(documentId);
        await _tableStorageRepository.DeleteDocumentAsync(documentId);
    }

    public async Task<Documents> GetDocumentByIdAsync(string documentId)
    {
        return await _tableStorageRepository.GetDocumentByIdAsync(documentId)
               ?? throw new KeyNotFoundException("Document is not found");
    }

    public async Task<IEnumerable<Documents>> GetAllDocumentsAsync()
    {
        return await _tableStorageRepository.GetAllDocumentsAsync();
    }

    public async Task<Stream> DownloadDocumentAsync(string documentId)
    {
        var document = await _tableStorageRepository.GetDocumentByIdAsync(documentId);
        if (document == null)
        {
            throw new KeyNotFoundException("Document is not found");
        }
        return await _blobStorageRepository.DownloadBlobAsync(documentId);
    }
    
    public async Task<string> GetDocumentUrlAsync(string documentId)
    {
        var documentUrl = await _tableStorageRepository.GetDocumentUrlAsync(documentId);

        if (string.IsNullOrEmpty(documentUrl))
        {
            throw new KeyNotFoundException("URL for this document was not found");
        }

        return documentUrl;
    }
    
    private string GenerateFileUrl(string documentId)
    {
        return $"https://mydomain.com/documents/{documentId}";
    }
}