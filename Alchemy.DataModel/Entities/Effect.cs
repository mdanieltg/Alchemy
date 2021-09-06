using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Alchemy.DataModel.Entities
{
    [DebuggerDisplay("Name: {Name}")]
    public class Effect
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(24)]
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}
