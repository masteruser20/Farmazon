using System;
using System.Linq;
using Farmazon.CartService.Domain;
using NUnit.Framework;

namespace Farmazon.CartService.Tests
{
    [TestFixture]
    public class CartTests
    {
        [Test]
        public void AddItem_ShouldAddItemToCart()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid());
            var cartItem = new CartItem(Guid.NewGuid(), cart.Id, Guid.NewGuid(), 5);

            // Act
            cart.AddItem(cartItem);

            // Assert
            Assert.Contains(cartItem, cart.Items);
        }

        [Test]
        public void FindItem_ShouldReturnItem_WhenItemExists()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();
            var cartItem = new CartItem(Guid.NewGuid(), cart.Id, productId, 5);
            cart.AddItem(cartItem);

            // Act
            var foundItem = cart.FindItem(productId);

            // Assert
            Assert.AreEqual(cartItem, foundItem);
        }

        [Test]
        public void FindItem_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();

            // Act
            var foundItem = cart.FindItem(productId);

            // Assert
            Assert.IsNull(foundItem);
        }
    }
}