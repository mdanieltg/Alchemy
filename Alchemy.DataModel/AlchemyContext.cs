using Alchemy.DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.DataModel
{
    public class AlchemyContext : DbContext
    {
        private const string ConnectionString = "Server=localhost;Database=SkyrimAlchemy;Trusted_Connection=True";

        public AlchemyContext()
        {
        }

        public AlchemyContext(DbContextOptions<AlchemyContext> options) : base(options)
        {
        }

        public DbSet<Dlc> Dlcs { get; set; }
        public DbSet<Effect> Effects { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
