using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Models;

public class Mix
{
    public required Effect Effect { get; init; }
    public required IEnumerable<Ingredient> Ingredients { get; init; }
}
