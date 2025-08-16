using Alchemy.DataModel;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public class DlcRepository : IDlcRepository
{
    private readonly AlchemyContext _context;

    public DlcRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public IEnumerable<Dlc> List()
    {
        return _context.Dlcs;
    }

    public Dlc? Get(int dlcId)
    {
        return _context.Dlcs
            .FirstOrDefault(dlc => dlc.Id == dlcId);
    }
}
