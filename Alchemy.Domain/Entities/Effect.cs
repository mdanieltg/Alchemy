using System.Diagnostics;

namespace Alchemy.Domain.Entities;

[DebuggerDisplay("{Name}")]
public class Effect
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public HashSet<Ingredient> Ingredients { get; set; } = new();
}
