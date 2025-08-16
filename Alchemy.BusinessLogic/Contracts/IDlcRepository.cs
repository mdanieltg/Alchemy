using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Contracts;

public interface IDlcRepository
{
    IEnumerable<Dlc> GetAll();
    Dlc Get(int dlcId);
}
