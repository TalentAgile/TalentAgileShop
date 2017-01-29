namespace TalentAgileShop.Model
{
    public class CartPrice
    {
        public decimal DeliveryCost { get; set; }

        public decimal ProductCost { get; set; }

        public bool InvalidDiscountCode { get; set; }
    }
}