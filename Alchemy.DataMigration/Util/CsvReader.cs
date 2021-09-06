using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alchemy.DataMigration.Models;

namespace Alchemy.DataMigration.Util
{
    public static class CsvReader
    {
        public static Task<IEnumerable<Ingredient>> ReadIngredients(string file)
        {
            return Task.Run(() =>
            {
                var ingredients = new List<Ingredient>();
                var lines = ReadFile(file);

                foreach (var rawLine in lines)
                {
                    var values = rawLine.Split(',', 4, StringSplitOptions.RemoveEmptyEntries);

                    ingredients.Add(new Ingredient
                    {
                        Name = values[0].Trim(),
                        Weight = double.Parse(values[1]),
                        BaseValue = int.Parse(values[2]),
                        Obtaining = values[3].Trim()
                    });
                }

                return (IEnumerable<Ingredient>)ingredients;
            });
        }

        public static Task<IEnumerable<Effect>> ReadEffects(string file)
        {
            return Task.Run(() =>
            {
                var effects = new List<Effect>();
                var lines = ReadFile(file);

                foreach (var line in lines)
                {
                    effects.Add(new Effect { Name = line.Trim() });
                }

                return (IEnumerable<Effect>)effects;
            });
        }

        public static Task<IEnumerable<IngredientEffects>> ReadIngredientEffects(string file)
        {
            return Task.Run(() =>
            {
                var ingredientEffects = new HashSet<IngredientEffects>();
                var lines = ReadFile(file);

                foreach (var rawLine in lines)
                {
                    var values = rawLine.Split(',', 5);
                    var ingredient = values[0].Trim();

                    foreach (var effect in values.Skip(1))
                    {
                        ingredientEffects.Add(new IngredientEffects
                        {
                            Ingredient = ingredient,
                            Effect = effect.Trim()
                        });
                    }
                }

                return (IEnumerable<IngredientEffects>)ingredientEffects;
            });
        }

        private static IEnumerable<string> ReadFile(string file)
        {
            using var reader = new StreamReader(file);
            var csvLine = reader.ReadLine();

            while ((csvLine = reader.ReadLine()) != null)
            {
                yield return csvLine;
            }

            reader.Close();
        }
    }
}
