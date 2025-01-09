using DocumentsService.Application.Interfaces;
using DocumentsService.Application.Services;
using DocumentsService.Core.Interfaces;
using DocumentsService.Infrastructure.Repositories;

namespace DocumentsService.Application.Configurations;

public static class DependencyInjectionSetup
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDocumentService, DocumentService>();

        services.AddScoped<IBlobStorageRepository>(provider =>
        {
            var connectionString = configuration["AzureStorage:BlobConnectionString"];
            var containerName = configuration["AzureStorage:BlobContainerName"];
            return new BlobStorageRepository(connectionString, containerName);
        });

        services.AddScoped<ITableStorageRepository>(provider =>
        {
            var connectionString = configuration["AzureStorage:TableConnectionString"];
            var tableName = configuration["AzureStorage:TableName"];
            return new TableStorageRepository(connectionString, tableName);
        });

        return services;
    }
}