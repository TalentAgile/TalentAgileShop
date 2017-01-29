using System.ComponentModel.DataAnnotations;

namespace TalentAgileShop.Model
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public ProductSize Size { get; set; } 

        public virtual Category Category { get; set; }

        
        public virtual Country Origin { get; set; }

        public decimal Price { get; set; }

        public virtual ProductImage Image { get; set; }

        public string Description { get; set; }

    }
}