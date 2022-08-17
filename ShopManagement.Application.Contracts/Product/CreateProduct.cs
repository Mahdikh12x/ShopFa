using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Name { get; set; } = null!;
       
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string ShortDescription { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Description { get; set; } = null!;

        [ExtensionFiles(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessages.FileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
      
        [Range(1,100000,ErrorMessage = ValidationMessages.Required)]
        public long CategoryId { get; set; }
      
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Keywords { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Slug { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string MetaDescription { get; set; } = null!;

        public List<ProductCategoryViewModel>? Categories { get; set; }
    }
}
