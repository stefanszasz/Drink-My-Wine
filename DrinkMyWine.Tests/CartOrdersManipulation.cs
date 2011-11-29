using System;
using System.Text;
using NUnit.Framework;
using DrinkMyWine;

namespace DrinkMyWine.Tests
{
    [TestFixture]
    public class CartCheckout
    {
        private Cart cart;
        private User validUser;

        [SetUp]
        public void CartInitializer()
        {
            validUser = User.Create("valid@useremail.com", "magicPass");
            cart = new Cart
                       {
                           User = validUser
                       };
        }

        [Test]
        public void CartCheckoutWithoutItems_Fails()
        {
            Assert.Throws<InvalidOperationException>(cart.CheckOut);
        }

        [Test]
        public void CartCheckoutWithOneItem_Succeeds()
        {
            var line = new OrderLine(new Wine());
            cart.AddLineToCart(line);
            cart.CheckOut();
            Assert.AreEqual(1, cart.TotalItems);
        }

        [Test]
        public void ChartCheckoutWithItems_NoWine_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderLine(null));
        }

        [Test]
        public void OrderLineCration_HasSingleQuantity()
        {
            var line = new OrderLine(new Wine());
            Assert.AreEqual(1, line.Quantity);
        }

        [Test]
        public void CartCheckout_NoWinesInStock_Fauls()
        {
            var line = new OrderLine(new Wine() { InStock = false });
            Assert.Throws<InvalidOperationException>(() => cart.AddLineToCart(line));
        }

        [Test]
        public void CartCheckoutPriceOK_OneLine()
        {
            var wineToBuy = new Wine { Name = "Great red wine" };
            var line = new OrderLine(wineToBuy)
                           {
                               Quantity = 5,
                               Price = 50
                           };
            cart.AddLineToCart(line);
            cart.CheckOut();
            Assert.AreEqual(250, cart.TotalPrice);
        }

        [Test]
        [TestCase(-6)]
        [TestCase(0)]
        public void CartAddLessOrZeroQuantityElements_Fails(int lessQuantity)
        {
            var wineToBuy = new Wine { Name = "Great red wine" };
            var line = new OrderLine(wineToBuy)
            {
                Quantity = lessQuantity,
                Price = 10
            };

            Assert.Throws<InvalidOperationException>(() => cart.AddLineToCart(line));
        }

        [Test]
        public void CartCheckout_ValidOrders_NoUser_Fails()
        {
            var wineToBuy = new Wine { Name = "Great red wine" };
            cart.User = null;
            cart.AddLineToCart(new OrderLine(wineToBuy) { Quantity = 2 });
            Assert.Throws<InvalidOperationException>(() => cart.CheckOut());
        }

        [Test]
        public void CartCheckout_ValidOrders_InvalidUser_Fails()
        {
            var wineToBuy = new Wine { Name = "Great red wine" };
            cart.AddLineToCart(new OrderLine(wineToBuy) { Quantity = 2 });
            cart.User = User.Create("aaa", "bbb");
            Assert.Throws<InvalidOperationException>(() => cart.CheckOut());
        }
    }
}
