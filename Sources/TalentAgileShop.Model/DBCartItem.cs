using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentAgileShop.Model
{
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