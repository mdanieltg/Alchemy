using Alchemy.WebApp.Validation;

namespace Alchemy.WebApp.Models;

public class ItemsSelection
{
    [GreaterThanZero]
    public ICollection<int> Ingredients { get; set; } = new List<int>();
}
