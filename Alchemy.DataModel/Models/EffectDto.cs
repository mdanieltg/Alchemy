using CsvHelper.Configuration.Attributes;

namespace Alchemy.DataModel.Models;

public class EffectDto
{
    [Index(0)]
    public int Id { get; set; }

    [Index(1)]
    public string Name { get; set; } = null!;
}
