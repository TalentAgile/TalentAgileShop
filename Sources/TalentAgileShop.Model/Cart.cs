using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [Table("CartItems")]
    public class DBCartItem
    {
        [Column("Cart_Id"), Key()]
        public string CartId { get; set; }
        [Key()]
        public Product Product { get; set; }

        public int Count { get; set; }

    }
}