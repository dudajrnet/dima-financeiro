using Microsoft.AspNetCore.Components.Authorization;

namespace Dima.Web.Sercurity;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}
