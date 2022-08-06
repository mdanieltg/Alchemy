using Alchemy.BusinessLogic.Contracts;
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

    public IEnumerable<Ingredient> GetAll()
    {
        return _context.Ingredients;
    }

    public Ingredient? Get(int ingredientId)
    {
        return _context.Ingredients
            .FirstOrDefault(ingredient => ingredient.Id == ingredientId);
    }
}
