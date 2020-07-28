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
        public IList<Product> Products { get; set; }

        public Cart()
        {
            Products = new List<Product>();
        }


        public void CreateCart(Guid cartId, DateTime createdAt)
        {
            var ev = new CartCreated(cartId, createdAt);
            AddEvent(ev);
        }

        public void AddProduct(Guid productId, int quantity)
        {
            var ev = new ProductAddedToCart(AggregateId, productId, quantity);
            AddEvent(ev);
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
            switch(@event)
            {
                case CartCreated c:
                    this.AggregateId = c.AggregateId;
                    this.CreatedAt = c.CreatedAt;
                    break;
                
                case ProductAddedToCart p:
                    this.Products.Add(new Product { AggregateId = p.ProductId });
                    break;
            }
        }
    }
}
