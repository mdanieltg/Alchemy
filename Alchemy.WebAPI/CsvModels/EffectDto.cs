using CsvHelper.Configuration.Attributes;

namespace Alchemy.WebAPI.CsvModels;

public class EffectDto
{
    [Index(0)]
    public int Id { get; set; }

    [Index(1)]
    public required string Name { get; set; }
}
