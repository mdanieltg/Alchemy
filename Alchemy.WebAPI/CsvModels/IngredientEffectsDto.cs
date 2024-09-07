﻿using CsvHelper.Configuration.Attributes;

namespace Alchemy.WebAPI.CsvModels;

public class IngredientEffectsDto
{
    [Index(1)]
    public int IngredientId { get; set; }

    [Index(0)]
    public int EffectId { get; set; }
}