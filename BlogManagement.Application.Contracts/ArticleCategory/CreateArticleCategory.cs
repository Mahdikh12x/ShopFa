using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Name { get;  set; }
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile? Picture { get;  set; }

        [MaxLength(255,ErrorMessage = ValidationMessages.MaxLength)]
        public string? PictureAlt { get;  set; }

        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string? PictureTitle { get;  set; }

        [MaxLength(2000,ErrorMessage = ValidationMessages.MaxLength)]
        public string? Description { get;  set; }

        [Range(1,int.MaxValue,ErrorMessage = ValidationMessages.Required)]
        public int ShowOrder { get;  set; }

        [MaxLength(100,ErrorMessage = ValidationMessages.MaxLength)]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MaxLength(300,ErrorMessage = ValidationMessages.MaxLength)]
        public string Slug { get;  set; }

        [MaxLength(150,ErrorMessage = ValidationMessages.MaxLength)]
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string MetaDescription { get;  set; }

        [MaxLength(1000,ErrorMessage = ValidationMessages.MaxLength)]
        public string? CanonicalAddress { get;  set; }
    }
}
