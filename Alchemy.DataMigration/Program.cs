using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alchemy.DataMigration.Util;
using Alchemy.DataModel;
using Alchemy.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.DataMigration
{
    internal class Program
    {
        private const string AlchemyDataPath = @"..\..\..\..\alchemy_data\";

        private static async Task Main(string[] args)
        {
            try
            {
                await PopulateDatabase();

                TestData();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        private static async Task PopulateDatabase()
        {
            var readEffects = await CsvReader.ReadEffects(AlchemyDataPath + "effects.csv");
            var readIngredients = await CsvReader.ReadIngredients(AlchemyDataPath + "ingredients.csv");
            var ingredientsEffects = await CsvReader.ReadIngredientEffects(AlchemyDataPath + "ingredients-effects.csv");

            using var alchemyContext = new AlchemyContext();
            using var transaction = await alchemyContext.Database.BeginTransactionAsync();

            try
            {
                // Add DLCs
                alchemyContext.Dlcs.Add(new Dlc { Name = "Dawnguard" });
                alchemyContext.Dlcs.Add(new Dlc { Name = "Hearthfire" });
                alchemyContext.Dlcs.Add(new Dlc { Name = "Dragonborn" });
                await alchemyContext.SaveChangesAsync();

                // Add effects
                foreach (var effect in readEffects)
                {
                    var model = new Effect
                    {
                        Name = effect.Name
                    };
                    alchemyContext.Add(model);
                }

                await alchemyContext.SaveChangesAsync();

                // Add ingredients with effects
                foreach (var ingredient in readIngredients)
                {
                    var model = new Ingredient
                    {
                        Name = ingredient.Name,
                        Weight = ingredient.Weight,
                        BaseValue = ingredient.BaseValue,
                        Obtaining = ingredient.Obtaining,
                        DlcId = (int?)ingredient.DlcId
                    };

                    // Add effects for current ingredient
                    foreach (var ingredientEffects in ingredientsEffects
                        .Where(e => e.Ingredient == model.Name))
                    {
                        var effect = alchemyContext.Effects.First(e => e.Name == ingredientEffects.Effect);
                        model.Effects.Add(effect);
                    }

                    alchemyContext.Ingredients.Add(model);
                }

                await alchemyContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private static void TestData()
        {
            using var context = new AlchemyContext();

            /* I have these ingredients...
            6	Poison Bloom
            27	Mudcrab Chitin
            44	Spawn Ash
            67	Briar Heart */
            var myIngredients = context.Ingredients
                .Include(i => i.Effects)
                .Where(ingredient =>
                    ingredient.Id == 6 || ingredient.Id == 27 || ingredient.Id == 44 || ingredient.Id == 67)
                .ToList();

            var effectsWithIngredients = new Dictionary<Effect, HashSet<Ingredient>>();

            foreach (var ingredient in myIngredients)
            {
                foreach (var effect in ingredient.Effects)
                {
                    if (!effectsWithIngredients.ContainsKey(effect))
                    {
                        effectsWithIngredients.Add(effect, new HashSet<Ingredient>() { ingredient });
                    }
                    else
                    {
                        effectsWithIngredients[effect].Add(ingredient);
                    }
                }
            }

            foreach (var pair in effectsWithIngredients
                .Where(pair => pair.Value.Count > 1)
                .OrderByDescending(pair => pair.Value.Count))
            {
                Console.WriteLine("Combine {0} for effect {1}",
                    string.Join(", ", pair.Value.Select(i => i.Name)),
                    pair.Key.Name);
            }
        }
    }
}
