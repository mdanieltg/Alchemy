using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class DownloadableContentRepository : IRepository<DownloadableContent>
{
    private readonly AlchemyContext _context;

    public DownloadableContentRepository(AlchemyContext alchemyContext)
    {
        _context = alchemyContext ?? throw new ArgumentNullException(nameof(alchemyContext));
    }

    public DownloadableContent? Get(int contentId) => _context.Dlcs.FirstOrDefault(content => content.Id == contentId);

    public IEnumerable<DownloadableContent> List() => _context.Dlcs;
}
