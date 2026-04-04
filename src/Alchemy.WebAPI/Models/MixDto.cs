namespace Alchemy.WebAPI.Models;

public class MixDto
{
    public EffectLimited Effect { get; set; } = null!;
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
}
