namespace Dima.Core.Requests.Transaction;

public class GetTransactionByPeriodRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
