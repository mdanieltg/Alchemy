using Alchemy.BusinessLogic.Models;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;

namespace Alchemy.WebAPI.Services;

public class EffectsRepository : IPagedEffectsRepository
{
    private readonly AlchemyContext _context;

    public EffectsRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public PagedCollection<Effect> List(int limit, int offset)
    {
        var effects = _context.Effects
            .Skip((offset - 1) * limit)
            .Take(limit);

        return new PagedCollection<Effect>
        {
            Limit = limit,
            Offset = offset,
            Collection = effects
        };
    }

    public Effect? Get(int effectId)
    {
        return _context.Effects
            .SingleOrDefault(effect => effect.Id == effectId);
    }
}
