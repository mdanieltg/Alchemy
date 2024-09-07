using Alchemy.DataModel;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;

namespace Alchemy.BusinessLogic.Services;

public class Mixer : IMixer
{
    private readonly IRepository<Ingredient> _ingredients;
    private readonly DataStore _dataStore;

    public Mixer(IRepository<Ingredient> ingredientsRepository, DataStore dataStore)
    {
        _ingredients = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
        _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
    }

    public List<Mix> Mix(IReadOnlySet<int> ingredientIds)
    {
        Dictionary<Effect, List<Ingredient>> effectIngredientsDictionary = new();

        // Find all ingredients provided in ingredientIds
        List<Ingredient> selectedIngredients = _ingredients.List()
            .Where(i => ingredientIds.Contains(i.Id))
            .ToList();

        // Fill effectIngredients dictionary 
        foreach (Ingredient ingredient in selectedIngredients)
        {
            IEnumerable<int> ingredientEffects = _dataStore.IngredientsEffects
                .Where(ie => ie.IngredientId == ingredient.Id)
                .Select(ie => ie.EffectId);

            IEnumerable<Effect> effects = _dataStore.Effects.Where(effect => ingredientEffects.Contains(effect.Id));

            foreach (Effect effect in effects)
            {
                if (effectIngredientsDictionary.ContainsKey(effect))
                {
                    effectIngredientsDictionary[effect].Add(ingredient);
                }
                else
                {
                    effectIngredientsDictionary.Add(effect, new() { ingredient });
                }
            }
        }

        return effectIngredientsDictionary
            .Where(pair => pair.Value.Count > 1)
            .OrderByDescending(pair => pair.Value.Count)
            .Select(pair => new Mix
            {
                Effect = pair.Key,
                Ingredients = pair.Value
            })
            .ToList();
    }
}
