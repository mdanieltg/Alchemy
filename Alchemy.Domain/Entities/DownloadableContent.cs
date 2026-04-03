using System.Diagnostics;

namespace Alchemy.Domain.Entities;

[DebuggerDisplay("DLC: {Name}")]
public class DownloadableContent
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
