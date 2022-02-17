using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static System.String;

namespace _0_Framework.Application
{
    public class ExtensionFilesAttribute:ValidationAttribute,IClientModelValidator
    {

        private readonly string[] _extensions;

        public ExtensionFilesAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtension = Path.GetExtension(file.FileName);
            return _extensions.Contains(fileExtension);

        }

        public void AddValidation(ClientModelValidationContext context)
        {
            
            //string validateExtensions = Join("/",_extensions.Select(x=>x.ToString()).ToArray());
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-validateExtensions",ErrorMessage);
            //context.Attributes.Add("data-val-ExtensionFiles", validateExtensions);
        }
    }
}
