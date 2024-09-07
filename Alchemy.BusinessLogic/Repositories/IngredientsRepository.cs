using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class IngredientsRepository : IRepository<Ingredient>, IPagedRepository<Ingredient>
{
    private readonly DataStore _dataStore;

    public IngredientsRepository(DataStore dataStore)
    {
        _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public PagedCollection<Ingredient> List(int limit, int offset)
    {
        IEnumerable<Ingredient> ingredients = _dataStore.Ingredients
            .OrderBy(ingredient => ingredient.Name)
            .Skip((offset - 1) * limit)
            .Take(limit);

        var lastPage = (int) Math.Ceiling(_dataStore.Ingredients.Count / (double) limit);

        return new PagedCollection<Ingredient>
        {
            Limit = limit,
            Offset = offset,
            PreviousPage = offset <= lastPage && offset > 1 ? offset - 1 : null,
            NextPage = offset < lastPage && offset >= 1 ? offset + 1 : null,
            LastPage = lastPage,
            Collection = ingredients
        };
    }

    public IEnumerable<Ingredient> List() => _dataStore.Ingredients.OrderBy(ingredient => ingredient.Name);

    public Ingredient? Get(int ingredientId) =>
        _dataStore.Ingredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);
}
