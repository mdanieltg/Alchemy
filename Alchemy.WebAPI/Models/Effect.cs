namespace Alchemy.WebAPI.Models;

public class Effect
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<IngredientLimited> Ingredients { get; set; } = null!;
}
