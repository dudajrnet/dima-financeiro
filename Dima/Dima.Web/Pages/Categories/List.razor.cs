using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
    #region Properties
    public bool IsBusy {get; set;} = false;
    public List<Category> Categories {get; set;} = [];
    public string SearchString {get; set;} = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ICategoryHandler Handler {get; set;} = null!;
    [Inject]
    public NavigationManager Navigation {get; set;} = null!;
    [Inject]
    public ISnackbar Snackbar {get; set;} = null!;
    [Inject]
    public IDialogService DialogService {get; set;} = null!;
    #endregion

    #region Methods
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoryRequest();
            var result = await Handler.GetAllAsync(request);
            if(result.IsSuccess)
            {
                Categories = result.Data ?? [];                
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
    public Func<Category, bool> Filter => category =>
    {        
        if(string.IsNullOrEmpty(SearchString))
            return true;
        
        if(category.Id.ToString().Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if(category.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            return true;
        
        if(category.Description is not null &&category.Description.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
            return true;
        
        return false;
    };
    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox(
            "Atenção",
            $"Ao prosseguir a categoria {title} será excluída. Esta é uma ação irreversível! Deseja continuar?",
            yesText: "Excluir", cancelText: "Cancelar"); 

        if(result == true)
            await OnDeleteAsync(id, title);
            StateHasChanged();  

    }
    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest {Id = id};
            await Handler.DeleteAsync(request);
            Categories.RemoveAll(category => category.Id == id);
            Snackbar.Add($"A categoria {title} foi excluída!", Severity.Success);
        }
        catch(Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }


    #endregion
}