using System.Diagnostics;

namespace Alchemy.WebAPI.Models;

[DebuggerDisplay("DLC: {Name}")]
public class DownloadableContentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
