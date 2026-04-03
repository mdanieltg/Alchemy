using System.Diagnostics;

namespace Alchemy.WebAPI.Models;

[DebuggerDisplay("Effect: {Name}")]
public class EffectLimited
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
