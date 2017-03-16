using System.Data.Entity;

namespace TalentAgileShop.Model
{
    public interface IDataContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Product> Products { get; set; }

        DbSet<ProductImage> ProductImages { get; set; }
#if false
        DbSet<DBCartItem> CartItems { get; set; }
#endif
        int SaveChanges();
    }
}