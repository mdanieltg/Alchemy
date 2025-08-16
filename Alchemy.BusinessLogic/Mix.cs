using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic;

public class Mix
{
    public Effect Effect { get; set; } = null!;
    public IEnumerable<Ingredient> Ingredients { get; set; } = null!;
}
