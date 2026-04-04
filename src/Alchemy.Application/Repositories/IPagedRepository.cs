using Alchemy.Application.Models;

namespace Alchemy.Application.Repositories;

public interface IPagedRepository<T>
{
    T? Get(int id);
    PagedCollection<T> List(int limit, int offset);
}
