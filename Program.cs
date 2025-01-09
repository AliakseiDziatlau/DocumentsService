using DocumentsService.Application.Configurations;
using DocumentsService.Application.Interfaces;
using DocumentsService.Application.Services;
using DocumentsService.Core.Interfaces;
using DocumentsService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigurationSetup.AddCustomConfiguration(builder.Configuration , builder.Environment);

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

SwaggerSetup.ConfigureSwagger(app);
MiddlewareSetup.ConfigureMiddleware(app);
app.Run();