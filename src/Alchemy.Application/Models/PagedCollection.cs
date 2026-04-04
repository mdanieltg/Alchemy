namespace Alchemy.Application.Models;

public class PagedCollection<T>
{
    public required int PageSize { get; init; }
    public required int CurrentPage { get; init; }
    public required int LastPage { get; init; }
    public int? NextPage { get; init; }
    public int? PreviousPage { get; init; }
    public required IEnumerable<T> Collection { get; init; }
}
