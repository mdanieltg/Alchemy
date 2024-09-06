namespace Alchemy.Domain.Repositories;

public interface IRepository<T>
{
    T? Get(int id);
    IEnumerable<T> List();
}
