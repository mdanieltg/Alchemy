using Alchemy.Domain.Models;

namespace Alchemy.Domain.Repositories;

public interface IPagedRepository<T>
{
    T? Get(int id);
    PagedCollection<T> List(int limit, int offset);
}
