namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public class CustomerDiscountSearch
{
    public long ProductId { get; set; }
    public string StartDate { get; private set; }
    public string EndDate{ get; private set; }
}