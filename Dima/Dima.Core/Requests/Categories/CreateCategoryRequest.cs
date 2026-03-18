using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : BaseRequest
{
    [Required(ErrorMessage ="O título não pode ser vazio.")]
    [MaxLength(80, ErrorMessage ="O título deve conter até 80 caracteres.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage ="A descrição não pode ser vazia.")]
    public string Description { get; set; } = string.Empty;
}

