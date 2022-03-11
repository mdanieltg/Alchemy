using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Contracts;

public interface IEffectsRepository
{
    IEnumerable<Effect> GetAll();
    Effect Get(int effectId);
}
