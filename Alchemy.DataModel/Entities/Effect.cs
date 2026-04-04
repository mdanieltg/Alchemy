using System.Diagnostics;

namespace Alchemy.DataModel.Entities;

[DebuggerDisplay("Effect: {Name}")]
public class Effect
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<Ingredient> Ingredients { get; set; } = null!;
}
