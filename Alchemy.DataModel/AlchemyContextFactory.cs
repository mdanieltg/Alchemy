using Alchemy.DataModel.Entities;
using Alchemy.DataModel.Models;

namespace Alchemy.DataModel;

public class AlchemyContextFactory
{
    public static async Task<AlchemyContext> CreateContext(CsvHelper csvHelper)
    {
        var dlc = await Transform(csvHelper.GetDlcs());
        var effects = await Transform(csvHelper.GetEffects());
        var ingredients = await Transform(csvHelper.GetIngredients(), dlc);
        await InsertEffectsIntoIngredients(csvHelper.GetIngredientEffects(), ingredients, effects);

        return new AlchemyContext(dlc, effects, ingredients);
    }

    private static async Task<HashSet<Dlc>> Transform(IAsyncEnumerable<DlcDto> dlcs)
    {
        var set = new HashSet<Dlc>();
        await foreach (var dlc in dlcs)
        {
            set.Add(new Dlc
            {
                Id = dlc.Id,
                Name = dlc.Name
            });
        }

        return set;
    }

    private static async Task<HashSet<Effect>> Transform(IAsyncEnumerable<EffectDto> effects)
    {
        var set = new HashSet<Effect>();
        await foreach (var effect in effects)
        {
            set.Add(new Effect
            {
                Id = effect.Id,
                Name = effect.Name
            });
        }

        return set;
    }

    private static async Task<HashSet<Ingredient>> Transform(IAsyncEnumerable<IngredientDto> ingredients,
        IReadOnlyCollection<Dlc> dlcs)
    {
        var set = new HashSet<Ingredient>();
        await foreach (var ingredient in ingredients)
        {
            set.Add(new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                BaseValue = ingredient.BaseValue,
                Weight = ingredient.Weight,
                Obtaining = ingredient.Obtaining,
                DlcId = ingredient.DlcId,
                Dlc = dlcs.First(dlc => dlc.Id == ingredient.DlcId)
            });
        }

        return set;
    }

    private static async Task InsertEffectsIntoIngredients(IAsyncEnumerable<IngredientEffects> ingredientEffects,
        IReadOnlyCollection<Ingredient> ingredients, IReadOnlyCollection<Effect> effects)
    {
        await foreach (var ingredientEffect in ingredientEffects)
        {
            var effect = effects.First(e => e.Id == ingredientEffect.EffectId);
            var ingredient = ingredients.First(i => i.Id == ingredientEffect.IngredientId);

            effect.Ingredients.Add(ingredient);
            ingredient.Effects.Add(effect);
        }
    }
}
