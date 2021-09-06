namespace Alchemy.WebAPI.Models
{
    public class IngredientLimited
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int BaseValue { get; set; }
        public string Obtaining { get; set; }
        public int? DlcId { get; set; }
    }
}
