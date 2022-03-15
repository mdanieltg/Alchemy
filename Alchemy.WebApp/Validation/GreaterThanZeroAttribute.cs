using System.ComponentModel.DataAnnotations;
using Alchemy.WebApp.Models;

namespace Alchemy.WebApp.Validation;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var count = (ItemsSelection)validationContext.ObjectInstance;

        if (count.Ingredients.Count <= 0)
        {
            return new ValidationResult("La cantidad de ingredientes debe ser mayor a cero.");
        }

        return ValidationResult.Success;
    }
}
