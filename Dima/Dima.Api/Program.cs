using Dima.Api.Data;
using Dima.Api.Handler;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler)
        => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapGet("/v1/categories/{id}", async (long id, ICategoryHandler handler)
        =>
{
    var request = new GetCategoryByIdRequest
    {
        Id = id,
        UserId = "teste@teste.com"
    };

    return await handler.GetByIdAsync(request);
})
    .WithName("Categories: GetById.")
    .WithSummary("Obter uma categoria")
    .Produces<Response<Category?>>();

app.MapGet("/v1/categories/", async (ICategoryHandler handler)
        =>
{
    var request = new GetAllCategoryRequest
    {        
        UserId = "teste@teste.com"
    };

    return await handler.GetAllAsync(request);
})
    .WithName("Categories: Get All.")
    .WithSummary("Obter todas as categorias de um usuário")
    .Produces<PagedResponse<List<Category>?>>();



app.MapPut("/v1/categories/{id}", async (
        long id, 
        UpdateCategoryRequest request, 
        ICategoryHandler handler)
        =>
        {
            request.Id = id;
            return await handler.UpdateAsync(request);
        })
    .WithName("Categories: Upadate")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category?>>();

app.MapDelete("/v1/categories/{id}", async (long id, ICategoryHandler handler)
        =>
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
                UserId = ""
            };

            return await handler.DeleteAsync(request);
        })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category?>>();

app.Run();

