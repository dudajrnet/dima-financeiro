using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties
    public bool IsBusy {get; set;} = false;
    public CreateCategoryRequest InputModel {get; set;} = new();
    #endregion

    #region Services
    [Inject]
    public ICategoryHandler Handler {get; set;} = null!;
    [Inject]
    public NavigationManager Navigation {get; set;} = null!;
    [Inject]
    public ISnackbar Snackbar {get; set;} = null!;
    #endregion
    #region Methods
    public async Task OnvalidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateAsync(InputModel);

            if(result.IsSuccess)
            {
                Snackbar.Add(result.Message ?? "Categoria criada com sucesso.", Severity.Success);
                Navigation.NavigateTo("/categorias");
            }
            else
            {
                Snackbar.Add(result.Message ?? "Falha ao criar a categoria.", Severity.Error);
            }
        }
        catch(Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);            
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}