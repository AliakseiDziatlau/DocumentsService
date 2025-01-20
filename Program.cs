using DocumentsService.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCustomConfiguration(builder.Environment);

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

app.ConfigureSwagger();
app.ConfigureMiddleware();
app.Run();