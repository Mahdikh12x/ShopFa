namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public class CustomerDiscountViewModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string StartDate { get; set; }
    public string Product { get; set; }
    public DateTime StartDateEnglish { get; set; }
    public string EndDate { get; set; }
    public DateTime EndDateEnglish { get; set; }
    public string Reason { get; set; }
    public int DiscountRate { get; set; }
}