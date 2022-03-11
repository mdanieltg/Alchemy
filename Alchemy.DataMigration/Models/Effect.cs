using System.Diagnostics;

namespace Alchemy.DataMigration.Models;

[DebuggerDisplay("{Name}")]
public class Effect
{
    public int Id { get; set; }
    public string Name { get; set; }
}
