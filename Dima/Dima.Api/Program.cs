using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handler;
using Dima.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder
       .Configuration
       .GetConnectionString("DefaultConnection")
       ?? throw new Exception("Connection string não encontrada."));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new { message = "OK" });

app.MapEndpoints();

app.Run();

