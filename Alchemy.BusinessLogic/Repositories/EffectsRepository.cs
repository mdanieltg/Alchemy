using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;

namespace Alchemy.BusinessLogic.Repositories;

public class EffectsRepository : IRepository<Effect>, IPagedRepository<Effect>
{
    private readonly DataStore _dataStore;

    public EffectsRepository(DataStore dataStore)
    {
        _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public PagedCollection<Effect> List(int limit, int offset)
    {
        IEnumerable<Effect> effects = _dataStore.Effects
            .OrderBy(effect => effect.Name)
            .Skip((offset - 1) * limit)
            .Take(limit);

        var lastPage = (int) Math.Ceiling(_dataStore.Effects.Count / (double) limit);

        return new PagedCollection<Effect>
        {
            Limit = limit,
            Offset = offset,
            PreviousPage = offset <= lastPage && offset > 1 ? offset - 1 : null,
            NextPage = offset < lastPage && offset >= 1 ? offset + 1 : null,
            LastPage = lastPage,
            Collection = effects
        };
    }

    public IEnumerable<Effect> List() => _dataStore.Effects.OrderBy(effect => effect.Name);

    public Effect? Get(int effectId)
    {
        var ingredientEffects = _dataStore.Ingredients
            .Join(_dataStore.IngredientsEffects,
                ingredient => ingredient.Id,
                ingredientsEffects => ingredientsEffects.IngredientId,
                (ingredient, ingredientsEffects) => new
                {
                    Ingredient = ingredient,
                    IngredientEffects = ingredientsEffects
                }
            )
            .Where(x => x.IngredientEffects.EffectId == effectId);

        return _dataStore.Effects
            .GroupJoin(ingredientEffects, effect => effect.Id,
                grouping => grouping.IngredientEffects.EffectId,
                (effect, ingredientGrouping) => new Effect
                {
                    Id = effect.Id,
                    Name = effect.Name,
                    Ingredients = ingredientGrouping
                        .Select(grouping => grouping.Ingredient)
                        .OrderBy(ingredient => ingredient.Name)
                }
            )
            .FirstOrDefault(effect => effect.Id == effectId);
    }
}
