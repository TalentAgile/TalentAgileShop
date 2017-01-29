using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; }

        public ProductViewModel(Product product)
        {
            Product = product;
        }


    }

}