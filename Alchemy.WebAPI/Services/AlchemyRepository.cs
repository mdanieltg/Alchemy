using Alchemy.DataModel;
using Alchemy.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.WebAPI.Services;

public class AlchemyRepository : IAlchemyRepository
{
    private readonly AlchemyContext _context;

    public AlchemyRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public Dlc GetDlc(int dlcId)
    {
        return _context.Dlcs.FirstOrDefault(dlc => dlc.Id == dlcId);
    }

    public IEnumerable<Dlc> GetDlcs()
    {
        return _context.Dlcs;
    }

    public Effect GetEffect(int effectId)
    {
        return _context.Effects
            .Include(effect => effect.Ingredients)
            .FirstOrDefault(effect => effect.Id == effectId);
    }

    public IEnumerable<Effect> GetEffects()
    {
        return _context.Effects.Include(effect => effect.Ingredients);
    }

    public Ingredient GetIngredient(int ingredientId)
    {
        return _context.Ingredients
            .Include(ingredient => ingredient.Effects)
            .Include(ingredient => ingredient.Dlc)
            .FirstOrDefault(ingredient => ingredient.Id == ingredientId);
    }

    public IEnumerable<Ingredient> GetIngredients()
    {
        return _context.Ingredients
            .Include(ingredient => ingredient.Effects)
            .Include(ingredient => ingredient.Dlc);
    }
}
