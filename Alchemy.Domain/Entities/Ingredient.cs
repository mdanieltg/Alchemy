using System.Diagnostics;

namespace Alchemy.Domain.Entities;

[DebuggerDisplay("Ingredient: {Name}")]
public class Ingredient
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Weight { get; set; }
    public int BaseValue { get; set; }
    public required string Obtaining { get; set; }
    public int? DlcId { get; set; }

    public DownloadableContent? Dlc { get; set; }
}
