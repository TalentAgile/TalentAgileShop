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
    public class InMemoryCartTests
    {

        [Test]
       
        public void Repository_Always_Returns_A_Non_Null_Cart()
        {
            var repository = new InMemoryCartRepository();


            var cart = repository.Get(Guid.NewGuid().ToString());

            Check.That(cart).IsNotNull();

        }

        [Test]
        public void Add_A_Product_On_A_Cart_Adds_A_ProductInfo_Item()
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
        public void Adding_A_Product_Twice_Adds_A_ProductInfo_Item_With_Two_Items()
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
        public void Adding_And_Then_Removing_A_Product_Twice_Does_Nothing()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

            repository.AddProduct(id, "product1");
            repository.RemoveProduct(id, "product1");


            var cart = repository.Get(id);

           
            Check.That(cart.Products).IsEmpty();

        }

        [Test]
        public void Removing_An_Non_Added_Product_Does_Nothing()
        {
            var repository = new InMemoryCartRepository();

            var id = Guid.NewGuid().ToString();

          
            repository.RemoveProduct(id, "product1");


            var cart = repository.Get(id);


            Check.That(cart.Products).IsEmpty();

        }
    }
}
