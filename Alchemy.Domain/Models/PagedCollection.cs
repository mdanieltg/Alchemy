namespace Alchemy.Domain.Models;

public class PagedCollection<T>
{
    public required int Limit { get; init; }
    public required int Offset { get; init; }
    public int LastPage { get; init; }
    public int? NextPage { get; init; }
    public int? PreviousPage { get; init; }
    public required IEnumerable<T> Collection { get; init; }
}
