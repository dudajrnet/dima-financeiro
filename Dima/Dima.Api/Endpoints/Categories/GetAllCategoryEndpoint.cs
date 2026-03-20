using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{pageNumber}/{pageSize}", HandleAsync)
          .WithName("Categories: Get All")
          .WithSummary("Obtém todas as categorias de um usuário")
          .WithDescription("Obtém todas as categorias de um usuário")
          .WithOrder(5)
          .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetAllCategoryRequest
        {
            UserId = Configuration.DefaultUserId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}

