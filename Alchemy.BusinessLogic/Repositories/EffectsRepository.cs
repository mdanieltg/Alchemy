using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class EffectsRepository : IRepository<Effect>, IPagedRepository<Effect>
{
    private readonly AlchemyContext _context;

    public EffectsRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public PagedCollection<Effect> List(int limit, int offset)
    {
        IEnumerable<Effect> effects = _context.Effects
            .Skip((offset - 1) * limit)
            .Take(limit);

        return new PagedCollection<Effect>
        {
            Limit = limit,
            Offset = offset,
            Collection = effects
        };
    }

    public IEnumerable<Effect> List() => _context.Effects.OrderBy(effect => effect.Name);

    public Effect? Get(int effectId) => _context.Effects.FirstOrDefault(effect => effect.Id == effectId);
}
