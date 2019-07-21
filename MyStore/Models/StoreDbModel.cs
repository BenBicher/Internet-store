namespace MyStore.Models
{
    using System.Data.Entity;

    public class StoreDbModel : DbContext
    {
        public StoreDbModel() : base("DefaultConnection") { }

        public DbSet<Product> Product { get; set; }
        public DbSet<RegisterViewModel> Register { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
    }
}