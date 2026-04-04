using Alchemy.DataModel;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Repositories;

public class DownloadableContentRepository(DataStore dataStore) : IRepository<DownloadableContent>
{
    private readonly DataStore _dataStore = dataStore;

    public DownloadableContent? Get(int contentId) =>
        _dataStore.Dlcs.FirstOrDefault(content => content.Id == contentId);

    public IEnumerable<DownloadableContent> List() => _dataStore.Dlcs;
}
