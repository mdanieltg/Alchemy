using Alchemy.BusinessLogic.Contracts;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.BusinessLogic.Services;

public class EffectsRepository : IEffectsRepository
{
    private readonly AlchemyContext _context;

    public EffectsRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public IEnumerable<Effect> GetAll()
    {
        return _context.Effects
            .Include(effect => effect.Ingredients);
    }

    public Effect Get(int effectId)
    {
        return _context.Effects
            .Include(effect => effect.Ingredients)
            .FirstOrDefault(effect => effect.Id == effectId);
    }
}
