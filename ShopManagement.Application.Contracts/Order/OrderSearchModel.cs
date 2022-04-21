namespace ShopManagement.Application.Contracts.Order;

public class OrderSearchModel
{
    public string? AccountName { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsPayed { get; set; }
    public long? RefId { get; set; }
}