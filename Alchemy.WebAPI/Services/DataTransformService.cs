using System.Collections.Immutable;
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

    public async ValueTask<DataStore> CreateDataStoreAsync()
    {
        HashSet<DlcDto> dlcDtos = await _csvHelper.GetDlcs();
        HashSet<IngredientDto> ingredientDtos = await _csvHelper.GetIngredients();
        HashSet<EffectDto> effectDtos = await _csvHelper.GetEffects();
        HashSet<IngredientsEffectsDto> ingredientEffectsDtos = await _csvHelper.GetIngredientEffects();

        ImmutableHashSet<DownloadableContent> downloadableContents = Transform(dlcDtos);
        ImmutableHashSet<Effect> effects = Transform(effectDtos);
        ImmutableHashSet<Ingredient> ingredients = Transform(ingredientDtos, downloadableContents);
        ImmutableHashSet<IngredientsEffects> ingredientsEffects = Transform(ingredientEffectsDtos);

        return new DataStore
        {
            Dlcs = downloadableContents,
            Effects = effects,
            Ingredients = ingredients,
            IngredientsEffects = ingredientsEffects
        };
    }

    private static ImmutableHashSet<DownloadableContent> Transform(IEnumerable<DlcDto> dlcs) =>
        dlcs.Select(dlcDto => new DownloadableContent
        {
            Id = dlcDto.Id,
            Name = dlcDto.Name
        }).ToImmutableHashSet();

    private static ImmutableHashSet<Effect> Transform(IEnumerable<EffectDto> effects) =>
        effects.Select(effectDto =>
            new Effect
            {
                Id = effectDto.Id,
                Name = effectDto.Name
            }
        ).ToImmutableHashSet();

    private static ImmutableHashSet<Ingredient> Transform(IEnumerable<IngredientDto> ingredients,
        IReadOnlySet<DownloadableContent> dlcs) =>
        ingredients.Select(ingredientDto =>
            new Ingredient
            {
                Id = ingredientDto.Id,
                Name = ingredientDto.Name,
                BaseValue = ingredientDto.BaseValue,
                Weight = ingredientDto.Weight,
                Obtaining = ingredientDto.Obtaining,
                DlcId = ingredientDto.DlcId,
                Dlc = dlcs.FirstOrDefault(dlc => dlc.Id == ingredientDto.DlcId)
            }
        ).ToImmutableHashSet();

    private static ImmutableHashSet<IngredientsEffects>
        Transform(IEnumerable<IngredientsEffectsDto> ingredientsEffects) =>
        ingredientsEffects.Select(dto => new IngredientsEffects
            {
                EffectId = dto.EffectId,
                IngredientId = dto.IngredientId
            }
        ).ToImmutableHashSet();
}
