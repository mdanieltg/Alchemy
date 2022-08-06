using System.ComponentModel.DataAnnotations;

namespace Alchemy.WebApp.Models;

public class ItemsSelection
{
    [Required]
    [MinLength(2, ErrorMessage = "Debe seleccionar al menos dos ingredientes.")]
    public ICollection<int>? Ingredients { get; set; } = new List<int>();
}
