using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Transactions;
using Dima.Core.Requests.Categories;
using System.Runtime.CompilerServices;

namespace Dima.Api.Endpoints;

public static class BaseEndpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app;

        endpoints
            .MapGroup("v1/categories")
            .WithTags("Categories")
            //.RequireAuthorization()
            .MapEndpoints<CreateCategoryEndpoint>()
            .MapEndpoints<GetAllCategoryEndpoint>()
            .MapEndpoints<GetByIdCategoryEndpoint>()
            .MapEndpoints<UpdateCategoryEndpoint>()
            .MapEndpoints<DeleteCategoryEndpoint>();

        endpoints
           .MapGroup("v1/transactions")
           .WithTags("Transactions")
           //.RequireAuthorization()
           .MapEndpoints<CreateTransactionEndpoint>()
           .MapEndpoints<GetTransactionsByPeriodEndpoint>()
           .MapEndpoints<GetTransactionByIdEndpoint>()
           .MapEndpoints<UpdateTransactionEndpoint>()
           .MapEndpoints<DeleteTransactionEndpoint>();

    }

    private static IEndpointRouteBuilder MapEndpoints<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);

        return app;
    }
}

