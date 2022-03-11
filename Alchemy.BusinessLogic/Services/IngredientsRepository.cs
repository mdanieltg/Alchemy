using Alchemy.BusinessLogic.Contracts;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

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
        return _context.Ingredients
            .Include(ingredient => ingredient.Effects)
            .Include(ingredient => ingredient.Dlc);
    }

    public Ingredient Get(int ingredientId)
    {
        return _context.Ingredients
            .Include(ingredient => ingredient.Effects)
            .Include(ingredient => ingredient.Dlc)
            .FirstOrDefault(ingredient => ingredient.Id == ingredientId);
    }
}
