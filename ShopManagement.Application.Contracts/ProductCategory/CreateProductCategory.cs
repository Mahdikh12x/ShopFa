using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Name { get; set; } = null!;

        [ExtensionFiles(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessages.FileFormat)]
        [MaxFileSize(3 * 1024 * 1024,ErrorMessage =ValidationMessages.MaxFileSize)]
        //[Required(ErrorMessage = ValidationMessages.Required)]
        public IFormFile? Picture { get; set; }

        public string? Description { get; set; }
        public string? PictureTitle { get; set; }
        public string? PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Slug { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Keywords { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string MetaDescription { get; set; } = null!;

    }
}