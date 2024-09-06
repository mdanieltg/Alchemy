using Alchemy.WebAPI.Models;

namespace Alchemy.WebAPI;

public class MixDto
{
    public EffectLimited Effect { get; set; } = null!;
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
}
