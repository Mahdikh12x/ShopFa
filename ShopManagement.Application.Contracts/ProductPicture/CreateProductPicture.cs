using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture;

public class CreateProductPicture
{
    [Range(1, 100000, ErrorMessage = ValidationMessages.Required)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessages.Required)]
    [ExtensionFiles(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessages.FileFormat)]
    [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessages.Required)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessages.Required)]
    public string PictureTitle { get; set; }

    public List<ProductViewModel> Products { get; set; }

}