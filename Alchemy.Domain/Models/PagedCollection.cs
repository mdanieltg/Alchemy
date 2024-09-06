namespace Alchemy.Domain.Models;

public class PagedCollection<T>
{
    public required int Limit { get; init; }
    public required int Offset { get; init; }
    public required IEnumerable<T> Collection { get; init; }
}
