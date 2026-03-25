
using Dima.Core.Enums;
using Dima.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transaction;

public class CreateTransactionRequest : BaseRequest
{
    [Required(ErrorMessage ="O campo título não pode ser vazio")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage ="Este é um campo obrigatório")]
    public DateTime? PaidOrReceivedAt { get; set; }

    [Required(ErrorMessage ="Tipo inválido.")]
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    [Required(ErrorMessage ="Este é um campo obrigatório.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage ="Categoria inválida.")]
    public long CategoryId { get; set; }        
}

