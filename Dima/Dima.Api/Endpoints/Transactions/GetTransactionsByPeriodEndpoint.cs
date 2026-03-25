using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", HandleAsync)
            .WithName("Transactions: Get All By Period")
            .WithSummary("Obtém transações pelo período")
            .WithDescription("Obtém transações pelo período")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();                            

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
       [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
       [FromQuery] int pageSize = Configuration.DefaultPageSize,
       [FromQuery] DateTime? startDate = null,
       [FromQuery] DateTime? endDate = null
        )
    {
        var request = new GetTransactionByPeriodRequest
        {
            UserId = Configuration.DefaultUserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriodAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }                                   
}
