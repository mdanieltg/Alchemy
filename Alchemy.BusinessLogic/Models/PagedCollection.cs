namespace Alchemy.BusinessLogic.Models;

public class PagedCollection<T>
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public IEnumerable<T> Collection { get; set; } = null!;
}
