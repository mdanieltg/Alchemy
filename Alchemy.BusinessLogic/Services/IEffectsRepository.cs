using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IEffectsRepository
{
    IEnumerable<Effect> List();
    Effect? Get(int effectId);
}
