namespace Alchemy.BusinessLogic.Repositories;

public interface IRepository<out T>
{
    T? Get(int id);
    IEnumerable<T> List();
}
