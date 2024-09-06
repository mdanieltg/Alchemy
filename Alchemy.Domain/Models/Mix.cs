using Alchemy.Domain.Entities;

namespace Alchemy.Domain.Models;

public class Mix
{
    public required Effect Effect { get; init; }
    public required IEnumerable<Ingredient> Ingredients { get; init; }
}
