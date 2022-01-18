using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide;

public class CreateSlide
{
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string Picture { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string PictureAlt { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string PictureTitle { get; set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string Heading { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string Title { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string Text { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string BtnText { get;  set; }
    [Required(ErrorMessage = ValidationMessages.Required)]
    public string Link { get; set; }
}