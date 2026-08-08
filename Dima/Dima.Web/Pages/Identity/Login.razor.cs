using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Sercurity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Dependencies
    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Inject]
    public IAccountHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    #endregion

    #region Properties
    public bool IsBusy { get; set; }
    public LoginRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
            Navigation.NavigateTo("/");
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess) 
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                SnackBar.Add(result.Message ?? string.Empty, Severity.Success);
                Navigation.NavigateTo("/");
            }
            else
                SnackBar.Add(result.Message ?? string.Empty, Severity.Error);
        }
        catch(Exception ex)
        {
            SnackBar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    } 
    #endregion
}
