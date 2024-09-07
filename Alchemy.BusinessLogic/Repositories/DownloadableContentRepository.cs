using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class DownloadableContentRepository : IRepository<DownloadableContent>
{
    private readonly DataStore _context;

    public DownloadableContentRepository(DataStore dataStore)
    {
        _context = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public DownloadableContent? Get(int contentId) => _context.Dlcs.FirstOrDefault(content => content.Id == contentId);

    public IEnumerable<DownloadableContent> List() => _context.Dlcs;
}
