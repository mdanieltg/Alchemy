using Alchemy.DataModel;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public class IngredientsRepository : IIngredientsRepository
{
    private readonly AlchemyContext _context;

    public IngredientsRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public IEnumerable<Ingredient> List()
    {
        return _context.Ingredients
            .OrderBy(i => i.Name);
    }

    public Ingredient? Get(int ingredientId)
    {
        return _context.Ingredients
            .FirstOrDefault(ingredient => ingredient.Id == ingredientId);
    }
}
