using System.Diagnostics;

namespace Alchemy.WebAPI.Models;

[DebuggerDisplay("Effect: {Name}")]
public class EffectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Url { get; set; }
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
}
