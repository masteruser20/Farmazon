using System;
using Farmazon.CartService.Domain;
using NUnit.Framework;

namespace Farmazon.CartService.Tests
{
    [TestFixture]
    public class CartItemTests
    {

        [Test]
        public void UpdateQuantity_ShouldUpdateQuantity_WhenNewQuantityIsPositive()
        {
            // Arrange
            var cartItem = new CartItem(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 5);
            var newQuantity = 10;

            // Act
            cartItem.UpdateQuantity(newQuantity);

            // Assert
            Assert.AreEqual(newQuantity, cartItem.Quantity);
        }
    }
}