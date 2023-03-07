using Microsoft.EntityFrameworkCore;

namespace FoodSaverWebApp.Entities
{
    public class FoodSaverDbContext : DbContext
    {
        public FoodSaverDbContext(DbContextOptions<FoodSaverDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<DiscountInfo> DiscountInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Ready for seed data when Entity models are developed fully
            modelBuilder.Entity<User>().HadData();
            modelBuilder.Entity<Business>().HadData();
            modelBuilder.Entity<Address>().HadData();
            modelBuilder.Entity<Items>().HadData();
            modelBuilder.Entity<DiscountInfo>().HadData();
            */
        }
    }
}
