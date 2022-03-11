using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Alchemy.DataModel.Entities;

[DebuggerDisplay("Name: {Name}")]
public class Ingredient
{
    public int Id { get; set; }

    [Required]
    [MaxLength(23)]
    public string Name { get; set; }

    public double Weight { get; set; }

    public int BaseValue { get; set; }

    [MaxLength(92)]
    public string Obtaining { get; set; }

    public int? DlcId { get; set; }

    public Dlc Dlc { get; set; }
    public ICollection<Effect> Effects { get; set; } = new HashSet<Effect>();
}
