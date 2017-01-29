namespace TalentAgileShop.Model
{
    public interface ICartRepository
    {
        Cart AddProduct(string cartId, string productId);
        Cart DeleteProduct(string cartId, string productId);
        Cart Get(string cartId);
        Cart RemoveProduct(string cartId, string productId);
    }
}