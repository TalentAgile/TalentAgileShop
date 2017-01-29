using System.ComponentModel.DataAnnotations;

namespace TalentAgileShop.Model
{
    public class Category
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}