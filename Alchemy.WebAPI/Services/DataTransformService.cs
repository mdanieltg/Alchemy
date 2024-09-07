using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.WebAPI.CsvModels;

namespace Alchemy.WebAPI.Services;

public class DataTransformService
{
    private readonly CsvHelperService _csvHelper;

    public DataTransformService(CsvHelperService csvHelper)
    {
        _csvHelper = csvHelper;
    }

    public async Task<DataStore> CreateContextAsync()
    {
        IAsyncEnumerable<DlcDto> dlcDtos = _csvHelper.GetDlcs();
        IAsyncEnumerable<IngredientDto> ingredientDtos = _csvHelper.GetIngredients();
        IAsyncEnumerable<EffectDto> effectDtos = _csvHelper.GetEffects();
        IAsyncEnumerable<IngredientEffectsDto> ingredientEffects = _csvHelper.GetIngredientEffects();

        HashSet<DownloadableContent> downloadableContents = await TransformAsync(dlcDtos);
        HashSet<Effect> effects = await TransformAsync(effectDtos);
        HashSet<Ingredient> ingredients = await TransformAsync(ingredientDtos, downloadableContents);

        await InsertEffectsIntoIngredientsAsync(
            ingredients: ingredients,
            effects: effects,
            ingredientEffects: ingredientEffects
        );

        return new DataStore
        {
            Dlcs = downloadableContents,
            Effects = effects,
            Ingredients = ingredients
        };
    }

    private static async Task<HashSet<DownloadableContent>> TransformAsync(IAsyncEnumerable<DlcDto> dlcs)
    {
        var set = new HashSet<DownloadableContent>();
        await foreach (DlcDto dlc in dlcs)
        {
            set.Add(new DownloadableContent
            {
                Id = dlc.Id,
                Name = dlc.Name
            });
        }

        return set;
    }

    private static async Task<HashSet<Effect>> TransformAsync(IAsyncEnumerable<EffectDto> effects)
    {
        var set = new HashSet<Effect>();
        await foreach (EffectDto effect in effects)
        {
            set.Add(new Effect
            {
                Id = effect.Id,
                Name = effect.Name
            });
        }

        return set;
    }

    private static async Task<HashSet<Ingredient>> TransformAsync(IAsyncEnumerable<IngredientDto> ingredients,
        IReadOnlyCollection<DownloadableContent> dlcs)
    {
        var set = new HashSet<Ingredient>();
        await foreach (IngredientDto ingredient in ingredients)
        {
            set.Add(new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                BaseValue = ingredient.BaseValue,
                Weight = ingredient.Weight,
                Obtaining = ingredient.Obtaining,
                DlcId = ingredient.DlcId,
                Dlc = dlcs.FirstOrDefault(dlc => dlc.Id == ingredient.DlcId)
            });
        }

        return set;
    }

    private static async Task InsertEffectsIntoIngredientsAsync(IReadOnlyCollection<Effect> effects,
        IReadOnlyCollection<Ingredient> ingredients, IAsyncEnumerable<IngredientEffectsDto> ingredientEffects)
    {
        await foreach (IngredientEffectsDto ingredientEffect in ingredientEffects)
        {
            Effect effect = effects.First(e => e.Id == ingredientEffect.EffectId);
            Ingredient ingredient = ingredients.First(i => i.Id == ingredientEffect.IngredientId);

            effect.Ingredients.Add(ingredient);
            ingredient.Effects.Add(effect);
        }
    }
}
