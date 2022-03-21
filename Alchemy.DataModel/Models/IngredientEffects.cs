using CsvHelper.Configuration.Attributes;

namespace Alchemy.DataModel.Models;

public class IngredientEffects
{
    [Index(1)]
    public int IngredientId { get; set; }

    [Index(0)]
    public int EffectId { get; set; }
}
