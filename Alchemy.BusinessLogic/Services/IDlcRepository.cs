using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IDlcRepository
{
    IEnumerable<Dlc> List();
    Dlc? Get(int dlcId);
}
