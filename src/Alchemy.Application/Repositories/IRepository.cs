namespace Alchemy.Application.Repositories;

public interface IRepository<out T>
{
    T? Get(int id);
    IEnumerable<T> List();
}
