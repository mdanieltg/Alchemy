using Alchemy.BusinessLogic.Models;

namespace Alchemy.BusinessLogic.Repositories;

public interface IPagedRepository<T>
{
    T? Get(int id);
    PagedCollection<T> List(int limit, int offset);
}
