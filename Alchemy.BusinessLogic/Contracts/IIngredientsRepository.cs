using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Contracts;

public interface IIngredientsRepository
{
    IEnumerable<Ingredient> GetAll();
    Ingredient Get(int ingredientId);
}
