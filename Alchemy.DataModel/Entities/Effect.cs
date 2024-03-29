﻿namespace Alchemy.DataModel.Entities;

public class Effect
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
}
