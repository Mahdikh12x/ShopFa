using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Slide;

public class CreateSlide
{
    [ExtensionFiles(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessages.FileFormat)]
    [MaxFileSize(5* 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile? Picture { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? PictureAlt { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? PictureTitle { get; set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? Heading { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? Title { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? Text { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? BtnText { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string? Link { get; set; }
}