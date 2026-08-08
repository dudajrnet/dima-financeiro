using Dima.Api.Common.Api;
using Dima.Api.Models;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Identity;

public class IdentityRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app
        .MapGet("/roles", Handle)
        .RequireAuthorization();

    private static Task<IResult> Handle(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(claim => new RoleClaim()
            {
                Issuer = claim.Issuer,
                OriginalIssuer = claim.OriginalIssuer,
                Type = claim.Type,
                Value = claim.Value,
                ValueType = claim.ValueType
            });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}
