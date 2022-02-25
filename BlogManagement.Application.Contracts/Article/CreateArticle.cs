using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Title { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MaxLength(1500,ErrorMessage = ValidationMessages.MaxLength)]
        public string ShortDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Body { get;  set; }

        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile? Picture { get;  set; }
        [MaxLength(255,ErrorMessage = ValidationMessages.MaxLength)]
        public string? PictureAlt { get;  set; }
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string? PictureTitle { get;  set; }
        public string PublishDate { get;  set; }

        [Range(1,long.MaxValue,ErrorMessage = ValidationMessages.Required)]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLength)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MaxLength(100,ErrorMessage = ValidationMessages.MaxLength)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [MaxLength(150,ErrorMessage = ValidationMessages.MaxLength)]
        public string MetaDescription { get;  set; }
        [MaxLength(1000,ErrorMessage = ValidationMessages.MaxLength)]
        public string? CanonicalAddress { get;  set; }
    }
}
