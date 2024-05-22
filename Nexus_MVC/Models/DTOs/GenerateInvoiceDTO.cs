using Nexus_MVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Nexus_MVC.Models.DTOs
{
    public class GenerateInvoiceDTO : IValidatableObject
    {
        [Required]
        public IFormFile OrderFile { get; set; }

        [Required]
        public IFormFile PriceFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errorMessage = Errors.AllowedXml();

            if (!FileHelper.IsXmlFile(OrderFile) || !FileHelper.IsXmlFile(PriceFile))
            {
                yield return new ValidationResult(errorMessage);
            }
        }
    }
}
