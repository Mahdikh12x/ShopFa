using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.Required)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string PictureTitle { get; set; }

        public List<ProductViewModel> Products { get; set; }

    }
}
