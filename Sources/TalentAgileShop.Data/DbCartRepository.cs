using System;
using System.Linq;
using TalentAgileShop.Model;
using System.Data.Entity;

namespace TalentAgileShop.Data
{
#if false
    public class DbCartRepository : ICartRepository
    {
        private readonly IDataContext _context;

        public DbCartRepository(IDataContext context)
        {
            _context = context;
        }



        public Cart AddProduct(string cartId, string productId)
        {
            var item = _context.CartItems.FirstOrDefault(_ => _.CartId == cartId && _.Product.Id == productId);
            if (item == null)
            {
                var product = _context.Products.FirstOrDefault(_ => _.Id == productId);
                if (product == null)
                {
                    throw new ArgumentException("Invalid product");
                }
                item = new DBCartItem()
                {
                    CartId = cartId,
                    Product = product,
                    Count = 0
                };
                _context.CartItems.Add(item);
            }

            item.Count++;

            _context.SaveChanges();


            return Get(cartId);
        }

        public Cart DeleteProduct(string cartId, string productId)
        {
            var item = _context.CartItems.FirstOrDefault(_ => _.CartId == cartId && _.Product.Id == productId);

            if (item != null)
            {

                _context.CartItems.Remove(item);

                _context.SaveChanges();
            }

            return Get(cartId);
        }

        public Cart Get(string cartId)
        {
            var products = (from cartItem in _context.CartItems.Include(_ => _.Product)
                            where cartItem.CartId == cartId
                            orderby cartItem.Product.Name
                            select new CartProduct() { Id = cartItem.Product.Id, Count = cartItem.Count }).ToList();

            var result = new Cart();
            result.Products.AddRange(products);

            return result;
        }

        public Cart RemoveProduct(string cartId, string productId)
        {
            var item = _context.CartItems.FirstOrDefault(_ => _.CartId == cartId && _.Product.Id == productId);

            if (item != null)
            {
                item.Count--;
                if (item.Count <= 0)
                {
                    _context.CartItems.Remove(item);
                }
                _context.SaveChanges();
            }

            return Get(cartId);
        }
    }
#endif
}