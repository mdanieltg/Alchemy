using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IPagedDlcRepository
{
    PagedCollection<Dlc> List(int limit, int offset);
    Dlc? Get(int dlcId);
}
