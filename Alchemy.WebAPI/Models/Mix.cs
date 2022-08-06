using Alchemy.WebAPI.Models;

namespace Alchemy.WebAPI;

public class Mix
{
    public EffectLimited Effect { get; set; } = null!;
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
}
