

using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.DAL
{
    public class FurnitureFactoryDbContext : DbContext
    {
        public FurnitureFactoryDbContext(DbContextOptions<FurnitureFactoryDbContext> options)
       : base(options)
        {
        }
        // tables
        public DbSet<Product> Products { get;set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Subcomponent> Subcomponents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FurnitureFactoryDbContext).Assembly);
        }

    }
}
