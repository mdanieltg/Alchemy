using System.Diagnostics;

namespace Alchemy.DataMigration.Models
{
    [DebuggerDisplay("{Name}")]
    public class Ingredient
    {
        private string _name;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                var lastChar = value[value.Length - 1];
                DlcId = lastChar switch
                {
                    '*' => Dlc.Dawnguard,
                    '†' => Dlc.Hearthfire,
                    '‡' => Dlc.Dragonborn,
                    _ => null
                };

                if (DlcId == null)
                {
                    _name = value;
                }
                else
                {
                    _name = value.Remove(value.Length - 1);
                }
            }
        }

        public double Weight { get; set; }
        public int BaseValue { get; set; }
        public string Obtaining { get; set; }
        public Dlc? DlcId { get; set; }
    }
}
