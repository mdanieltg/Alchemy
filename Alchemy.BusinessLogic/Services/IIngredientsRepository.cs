using Alchemy.BusinessLogic.Models;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public interface IIngredientsRepository
{
    IEnumerable<Ingredient> List();
    Ingredient? Get(int ingredientId);
}
