using System.Collections.Generic;

namespace Alchemy.WebAPI.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int BaseValue { get; set; }
        public string Obtaining { get; set; }
        public string Dlc { get; set; }
        public IEnumerable<EffectLimited> Effects { get; set; }
    }
}
