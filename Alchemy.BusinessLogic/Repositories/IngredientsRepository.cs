using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class IngredientsRepository : IRepository<Ingredient>, IPagedRepository<Ingredient>
{
    private readonly DataStore _context;

    public IngredientsRepository(DataStore dataStore)
    {
        _context = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public PagedCollection<Ingredient> List(int limit, int offset)
    {
        IEnumerable<Ingredient> ingredients = _context.Ingredients
            .Skip((offset - 1) * limit)
            .Take(limit);

        return new PagedCollection<Ingredient>
        {
            Limit = limit,
            Offset = offset,
            Collection = ingredients
        };
    }

    public IEnumerable<Ingredient> List() => _context.Ingredients.OrderBy(ingredient => ingredient.Name);

    public Ingredient? Get(int ingredientId) =>
        _context.Ingredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);
}
