using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace TalentAgileShop.Cart.Tests
{
    [TestFixture]
    public class InMemoryCartShould
    {

        [Test]
       
        public void return_a_not_null_cart()
        {
            var repository = new InMemoryCartRepository();


            var cart = repository.Get(Guid.NewGuid().ToString());

            Check.That(cart).IsNotNull();

        }

        [Test]
        public void add_product_info_when_a_product_is_added_into_the_cart()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

            repository.AddProduct(id, "product1");

            var cart = repository.Get(id);

            var productInfo = cart.Products.FirstOrDefault(p => p.Id == "product1");

            Check.That(productInfo).IsNotNull();
            Check.That(productInfo.Count).IsEqualTo(1);



        }

        [Test]
        public void add_product_info_items_when_a_product_is_added_twice_into_the_cart()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

            repository.AddProduct(id, "product1");
            repository.AddProduct(id, "product1");


            var cart = repository.Get(id);

            var productInfo = cart.Products.FirstOrDefault(p => p.Id == "product1");

            Check.That(productInfo).IsNotNull();
            Check.That(productInfo.Count).IsEqualTo(2);
        }

        [Test]
        public void do_nothing_when_a_product_is_added_and_then_removed_twice()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

            repository.AddProduct(id, "product1");
            repository.RemoveProduct(id, "product1");


            var cart = repository.Get(id);

           
            Check.That(cart.Products).IsEmpty();

        }

        [Test]
        public void do_nothing_when_trying_to_remove_a_non_added_product()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

          
            repository.RemoveProduct(id, "product1");


            var cart = repository.Get(id);


            Check.That(cart.Products).IsEmpty();

        }
    }
}
