namespace Alchemy.WebAPI.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int BaseValue { get; set; }
    public string Obtaining { get; set; } = null!;
    public string Dlc { get; set; } = null!;
    public IEnumerable<EffectLimited> Effects { get; set; } = null!;
}
