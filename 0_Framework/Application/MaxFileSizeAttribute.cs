using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class MaxFileSizeAttribute:ValidationAttribute/*,IClientModelValidator*/
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize} bytes.";
        }

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    context.Attributes.Add("data-val", "true");
        //    context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        //}
        //public override bool IsValid(object? value)
        //{
        //    var file = value as IFormFile;
        //    if (file == null) return true;
        //    return file.Length <= _maxFileSize;
        //}
        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        //}
    }
}
