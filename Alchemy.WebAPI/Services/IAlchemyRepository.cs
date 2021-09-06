using System.Collections.Generic;
using Alchemy.DataModel.Entities;

namespace Alchemy.WebAPI.Services
{
    public interface IAlchemyRepository
    {
        Dlc GetDlc(int dlcId);
        IEnumerable<Dlc> GetDlcs();

        Effect GetEffect(int effectId);
        IEnumerable<Effect> GetEffects();

        Ingredient GetIngredient(int ingredientId);
        IEnumerable<Ingredient> GetIngredients();
    }
}
