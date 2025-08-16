using Alchemy.BusinessLogic.Models;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;

namespace Alchemy.WebAPI.Services;

public class IngredientsRepository : IPagedIngredientsRepository
{
    private readonly AlchemyContext _context;

    public IngredientsRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public PagedCollection<Ingredient> List(int limit, int offset)
    {
        var ingredients = _context.Ingredients
            .Skip((offset - 1) * limit)
            .Take(limit);

        return new PagedCollection<Ingredient>
        {
            Limit = limit,
            Offset = offset,
            Collection = ingredients
        };
    }

    public Ingredient? Get(int ingredientId)
    {
        return _context.Ingredients
            .SingleOrDefault(ingredient => ingredient.Id == ingredientId);
    }
}
