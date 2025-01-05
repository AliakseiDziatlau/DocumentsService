using DocumentsService.Application.Interfaces;
using DocumentsService.Application.Services;
using DocumentsService.Core.Interfaces;
using DocumentsService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var enviroment = builder.Environment.EnvironmentName;
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();


//DI
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IBlobStorageRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration["AzureStorage:BlobConnectionString"];
    var containerName = configuration["AzureStorage:BlobContainerName"];
    return new BlobStorageRepository(connectionString, containerName);
});

builder.Services.AddScoped<ITableStorageRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration["AzureStorage:TableConnectionString"];
    var tableName = configuration["AzureStorage:TableName"];
    return new TableStorageRepository(connectionString, tableName);
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); 
    });
}    

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();