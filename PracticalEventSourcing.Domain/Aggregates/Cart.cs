using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Domain.Aggregates
{
    public class Cart : AggregateRoot
    {
        public DateTime CreatedAt { get; set; }
        public string ShippingEmail { get; set; }
        public IEnumerable<Product> Products { get; set; }


        public void CreateCart(Guid cartId, DateTime createdAt)
        {
            var ev = new CartCreated(cartId, createdAt);
            AddEvent(ev);
        }

        public void AddProduct()
        {
            var ev = new ProductAddedToCart();
        }

        public void RemoveProduct()
        {
            var ev = new ProductRemovedFromCart();
        }

        public void ChangeProductQuantity()
        {
            var ev = new CartProductQuantityChanged();
        }

        public void AddShippingEmail()
        {
            var ev = new ShippingEmailAddedToCart();
        }



        public override void ApplyEvent(IEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
