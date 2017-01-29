using System.Collections.Generic;
using System.Linq;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart
{
    public class InMemoryCartRepository : ICartRepository
    {
        private readonly Dictionary<string, Model.Cart> _carts;

        public InMemoryCartRepository()
        {
            _carts = new Dictionary<string, Model.Cart>();
        }


        public Model.Cart AddProduct(string cartId, string productId)
        {
            lock (_carts)
            {
                var cart = GetCore(cartId);

                var product = cart.Products.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    product = new CartProduct()
                    {
                        Id = productId
                    };

                    cart.Products.Add(product);

                }

                product.Count++;
                return new Model.Cart(cart);
            }
        }

        public Model.Cart RemoveProduct(string cartId, string productId)
        {
            lock (_carts)
            {
                var cart = GetCore(cartId);

                var product = cart.Products.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    return new Model.Cart(cart);

                }

                product.Count--;

                if (product.Count <= 0)
                {
                    cart.Products.Remove(product);
                }

                return new Model.Cart(cart);
            }
        }

        public Model.Cart DeleteProduct(string cartId, string productId)
        {
            lock (_carts)
            {
                var cart = GetCore(cartId);

                var product = cart.Products.FirstOrDefault(p => p.Id == productId);

                if (product != null)
                {
                    cart.Products.Remove(product);
                }

                return new Model.Cart(cart);
            }
        }

        private Model.Cart GetCore(string cartId)
        {
            Model.Cart result;

            var found = _carts.TryGetValue(cartId, out result);

            if (!found)
            {
                result = new Model.Cart();

                _carts.Add(cartId,result);
            }

            return result;

        }


        public Model.Cart Get(string cartId)
        {
            lock (_carts)
            {
                var cart = GetCore(cartId);

                return new Model.Cart(cart);
            }
         
        }



    }
}