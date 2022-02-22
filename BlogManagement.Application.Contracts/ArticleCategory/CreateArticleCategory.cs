using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Name { get;  set; }
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile? Picture { get;  set; }
        public string? PictureAlt { get;  set; }
        public string? PictureTitle { get;  set; }
        public string? Description { get;  set; }

        [Range(1,int.MaxValue,ErrorMessage = ValidationMessages.Required)]
        public int ShowOrder { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string MetaDescription { get;  set; }

        public string? CanonicalAddress { get;  set; }
    }
}
