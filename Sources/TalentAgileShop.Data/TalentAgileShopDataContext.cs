using System.Data.Entity;
using TalentAgileShop.Model;

namespace TalentAgileShop.Data
{
    public class TalentAgileShopDataContext : DbContext, IDataContext
    {

        public TalentAgileShopDataContext() : base("ShopDataContext")
        {


        }


        public DbSet<Country> Countries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

#if false
        public DbSet<DBCartItem> CartItems { get; set; }
#endif
 
    }
}

