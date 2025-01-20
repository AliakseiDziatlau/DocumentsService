using DocumentsService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;
    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty or does not exist");
        }

        using var stream = file.OpenReadStream();
        var documentId = await _documentService.UploadDocumentAsync(file.FileName, stream);
        return Ok(new { DocumentId = documentId });
    }
    
    [HttpDelete("{documentId}")]
    public async Task<IActionResult> DeleteDocument(string documentId)
    {
        await _documentService.DeleteDocumentAsync(documentId);
        return NoContent();
    }
    
    [HttpGet("{documentId}")]
    public async Task<IActionResult> GetDocument(string documentId)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId);
        return Ok(document);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDocuments()
    {
        var documents = await _documentService.GetAllDocumentsAsync();
        return Ok(documents);
    }
    
    [HttpGet("{documentId}/download")]
    public async Task<IActionResult> DownloadDocument(string documentId)
    {
        var stream = await _documentService.DownloadDocumentAsync(documentId);
        var fileName = $"{documentId}.file";
        return File(stream, "application/octet-stream", fileName);
    }
}