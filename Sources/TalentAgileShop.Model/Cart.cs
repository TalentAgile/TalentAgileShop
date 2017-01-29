using System.Collections.Generic;
using System.Linq;

namespace TalentAgileShop.Model
{
    public class Cart
    {
        
        public List<CartProduct> Products { get; }

        public Cart()
        {
            Products = new List<CartProduct>();
        }

        public Cart(Cart cart):this()
        {
            Products.AddRange(cart.Products.Select(p => new CartProduct() {Count = p.Count, Id = p.Id}));
        }
    }
}