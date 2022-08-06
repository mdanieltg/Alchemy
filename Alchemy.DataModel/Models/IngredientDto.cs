using CsvHelper.Configuration.Attributes;

namespace Alchemy.DataModel.Models;

public class IngredientDto
{
    [Index(0)]
    public int Id { get; set; }

    [Index(1)]
    public string Name { get; set; } = null!;

    [Index(2)]
    public double Weight { get; set; }

    [Index(3)]
    public int BaseValue { get; set; }

    [Index(4)]
    public string Obtaining { get; set; } = null!;

    [Index(5)]
    public int? DlcId { get; set; }
}
