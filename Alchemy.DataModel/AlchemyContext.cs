using Alchemy.DataModel.Entities;

namespace Alchemy.DataModel;

public class AlchemyContext
{
    internal AlchemyContext(HashSet<Dlc> dlcs, HashSet<Effect> effects, HashSet<Ingredient> ingredients)
    {
        Dlcs = dlcs;
        Effects = effects;
        Ingredients = ingredients;
    }

    public ICollection<Dlc> Dlcs { get; }
    public ICollection<Effect> Effects { get; }
    public ICollection<Ingredient> Ingredients { get; }
}
