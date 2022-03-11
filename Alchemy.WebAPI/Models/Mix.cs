using Alchemy.WebAPI.Models;

namespace Alchemy.WebAPI;

public class Mix
{
    public EffectLimited Effect { get; set; }
    public IEnumerable<IngredientLimited> Ingredients { get; set; }
}
