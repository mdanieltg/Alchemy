using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Alchemy.DataModel.Entities;

[DebuggerDisplay("Name: {Name}")]
public class Dlc
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Name { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
}
