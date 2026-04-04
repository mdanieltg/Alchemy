using System.Collections.Immutable;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.CsvModels;
using Mapster;

namespace Alchemy.WebAPI.Services;

public static class DataTransformService
{
    public static async ValueTask<DataStore> CreateDataStoreAsync(CsvHelperService csvHelper)
    {
        IEnumerable<DlcDto> dlcs = await csvHelper.GetDlcs();
        IEnumerable<IngredientDto> ingredients = await csvHelper.GetIngredients();
        IEnumerable<EffectDto> effects = await csvHelper.GetEffects();
        IEnumerable<IngredientsEffectsDto> ingredientEffects = await csvHelper.GetIngredientEffects();

        return new DataStore
        {
            Dlcs = dlcs.Adapt<IEnumerable<DownloadableContent>>().ToImmutableHashSet(),
            Effects = effects.Adapt<IEnumerable<Effect>>().ToImmutableHashSet(),
            Ingredients = ingredients.Adapt<IEnumerable<Ingredient>>().ToImmutableHashSet(),
            IngredientsEffects = ingredientEffects.Adapt<IEnumerable<IngredientsEffects>>().ToImmutableHashSet()
        };
    }
}
