namespace Alchemy.WebAPI.Models;

public class EffectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
    public string? Url { get; set; }
}
