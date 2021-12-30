﻿using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage =ValidationMessages.Required)]
        public string Name { get; set; }
        
        public string Picture { get; set; }
        public string Description { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Slug { get; set; }

        [Required(ErrorMessage =ValidationMessages.Required)]
        public string Keywords { get; set; }
        
        [Required(ErrorMessage =ValidationMessages.Required)]
        public string MetaDescription { get; set; }
    }
}