using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IPagedEffectsRepository
{
    PagedCollection<Effect> List(int limit, int offset);
    Effect? Get(int effectId);
}
