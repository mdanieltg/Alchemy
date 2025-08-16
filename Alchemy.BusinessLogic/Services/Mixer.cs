using Alchemy.BusinessLogic.Contracts;
using Alchemy.DataModel.Entities;

namespace Alchemy.BusinessLogic.Services;

public class Mixer : IMixer
{
    private readonly IIngredientsRepository _ingredients;

    public Mixer(IIngredientsRepository ingredientsRepository)
    {
        _ingredients = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
    }

    public Task<List<Mix>> Mix(IEnumerable<int> ingredientIds)
    {
        return Task.Run(() =>
        {
            var mixes = new List<Mix>();
            var ingredients = new List<Ingredient>();
            var effectIngredientsDictionary =
                new Dictionary<Effect, List<Ingredient>>();
            var allIngredients = _ingredients.GetAll().ToList();

            // Find all ingredients provided in ingredientIds
            foreach (var ingredientId in ingredientIds)
            {
                var ingredient = allIngredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);

                if (ingredient != null)
                {
                    ingredients.Add(ingredient);
                }
            }

            // Fill effectIngredients dictionary
            foreach (var ingredient in ingredients)
            {
                foreach (var effect in ingredient.Effects)
                {
                    if (effectIngredientsDictionary.ContainsKey(effect))
                    {
                        effectIngredientsDictionary[effect].Add(ingredient);
                    }
                    else
                    {
                        effectIngredientsDictionary.Add(effect,
                            new List<Ingredient>() { ingredient });
                    }
                }
            }

            // Add to the list the available ingredients to mix
            foreach (var keyValuePair in effectIngredientsDictionary
                         .Where(pair => pair.Value.Count > 1)
                         .OrderByDescending(pair => pair.Value.Count))
            {
                mixes.Add(new Mix
                {
                    Effect = keyValuePair.Key,
                    Ingredients = keyValuePair.Value
                });
            }

            return mixes;
        });
    }
}
