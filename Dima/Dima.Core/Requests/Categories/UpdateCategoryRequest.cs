using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : BaseRequest
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O título não pode ser vazio.")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A descrição não pode ser vazia.")]
    public string Description { get; set; } = string.Empty;
}


