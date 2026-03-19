using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handler;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
           
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            
            
            return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

            return new Response<Category?>(null, 500, "A001 - Falha ao criar a categoria.");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        try
        {
            var query = context
                .Categories
                .AsNoTracking()
                .Where(category => category.UserId == request.UserId);                

            var categories = await query
                .Skip((request.PageNumber) * request.PageSize)
                .Take(request.PageSize)                
                .ToListAsync();
            
            var count = await query.CountAsync();

            return new PagedResponse<List<Category>>(
                categories, 
                count, 
                request.PageNumber,
                request.PageSize                
                );
        }
        catch (Exception)
        {
            return new PagedResponse<List<Category>>(null, 500, "Falha ao obter as categorias.");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(category =>
                    category.Id == request.Id &&
                    category.UserId == request.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Categoria não encontrada")
                    : new Response<Category?>(category, 200, "Categoria encontrada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

            return new Response<Category?>(null, 500, "A004 - Falha ao excluir a categoria.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(category =>
                    category.Id == request.Id &&
                    category.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria atualizada com sucesso");
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

            return new Response<Category?>(null, 500, "A002 - Falha ao atualizar a categoria.");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(category =>
                    category.Id == request.Id &&
                    category.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");       

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria excluída com sucesso");
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

            return new Response<Category?>(null, 500, "A003 - Falha ao excluir a categoria.");
        }
    }  
}

