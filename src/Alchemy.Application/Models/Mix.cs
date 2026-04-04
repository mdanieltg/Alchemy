using Alchemy.DataModel.Entities;

namespace Alchemy.Application.Models;

public class Mix
{
    public required Effect Effect { get; init; }
    public required IEnumerable<Ingredient> Ingredients { get; init; }
}
