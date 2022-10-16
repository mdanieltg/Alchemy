using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IPagedIngredientsRepository
{
    PagedCollection<Ingredient> List(int limit, int offset);
    Ingredient? Get(int ingredientId);
}
