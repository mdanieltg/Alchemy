namespace Alchemy.DataModel.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int BaseValue { get; set; }
    public string Obtaining { get; set; } = null!;
    public int? DlcId { get; set; }

    public Dlc Dlc { get; set; } = null!;
    public ICollection<Effect> Effects { get; set; } = new HashSet<Effect>();
}
