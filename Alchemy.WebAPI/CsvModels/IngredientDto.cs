using CsvHelper.Configuration.Attributes;

namespace Alchemy.WebAPI.CsvModels;

public class IngredientDto
{
    [Index(0)]
    public int Id { get; set; }

    [Index(1)]
    public required string Name { get; set; }

    [Index(2)]
    public double Weight { get; set; }

    [Index(3)]
    public int BaseValue { get; set; }

    [Index(4)]
    public required string Obtaining { get; set; }

    [Index(5)]
    public int? DlcId { get; set; }
}
