using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.CustomerDiscount;

public class DefineCustomerDiscount
{
    [Range(1, 100000, ErrorMessage = ValidationMessages.Required)]
    public long ProductId { get; set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string StartDate { get; set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string EndDate { get; set; }
    public string Reason { get; set; }
    [Range(1, 99, ErrorMessage = ValidationMessages.Required)]
    public int DiscountRate { get; set; }
    public List<ProductViewModel> Products { get; set; }
}