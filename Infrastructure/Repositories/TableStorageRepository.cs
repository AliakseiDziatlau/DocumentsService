using Azure.Data.Tables;
using DocumentsService.Core.Entities;
using DocumentsService.Core.Interfaces;

namespace DocumentsService.Infrastructure.Repositories;

public class TableStorageRepository : ITableStorageRepository
{
    private readonly TableClient _tableClient;
    
    public TableStorageRepository(string connectionString, string tableName)
    {
        _tableClient = new TableClient(connectionString, tableName);
        _tableClient.CreateIfNotExists();
    }
    
    public async Task UpsertDocumentAsync(Documents document)
    {
        var entity = new TableEntity
        {
            PartitionKey = "Documents",
            RowKey = document.Id,
            ["Url"] = document.Url
        };

        await _tableClient.UpsertEntityAsync(entity);
    }

    public async Task DeleteDocumentAsync(string id)
    {
        await _tableClient.DeleteEntityAsync("Documents", id);
    }

    public async Task<Documents> GetDocumentByIdAsync(string id)
    {
        var entity = await _tableClient.GetEntityAsync<TableEntity>("Documents", id);
        return new Documents
        {
            Id = entity.Value.RowKey,
            Url = entity.Value["Url"].ToString()
        };
    }

    public async Task<IEnumerable<Documents>> GetAllDocumentsAsync()
    {
        var entities = _tableClient.Query<TableEntity>(filter: $"PartitionKey eq 'Documents'");
        return entities.Select(entity => new Documents
        {
            Id = entity.RowKey,
            Url = entity["Url"].ToString()
        });
    }

    public async Task<string> GetDocumentUrlAsync(string id)
    {
        var entity = await _tableClient.GetEntityAsync<TableEntity>("Documents", id);
        return entity.Value["Url"].ToString();
    }
}