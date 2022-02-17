using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.Required)]
        public long ProductId { get; set; }
        [Range(1, 99, ErrorMessage = ValidationMessages.Required)]
        public int DiscountRate { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}
