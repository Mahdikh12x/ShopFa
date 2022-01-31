using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Name { get; set; }
       
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string ShortDescription { get; set; }
       
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Description { get; set; }
       
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
      
        [Range(1,100000,ErrorMessage = ValidationMessages.Required)]
        public long CategoryId { get; set; }
      
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Keywords { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Slug { get; set; }
      
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string MetaDescription { get; set; }
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
