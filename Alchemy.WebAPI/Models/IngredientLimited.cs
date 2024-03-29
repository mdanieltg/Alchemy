﻿namespace Alchemy.WebAPI.Models;

public class IngredientLimited
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int BaseValue { get; set; }
    public string Obtaining { get; set; } = null!;
    public int? DlcId { get; set; }
}
