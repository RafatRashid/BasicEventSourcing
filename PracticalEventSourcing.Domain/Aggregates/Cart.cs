using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void RemoveProduct(Guid productId, int quantity)
        {
            var ev = new ProductRemovedFromCart(AggregateId, productId, quantity);
            AddEvent(ev);
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

                case ProductRemovedFromCart p:
                    var removedProduct = this.Products.FirstOrDefault(x => x.AggregateId.Equals(p.ProductId));
                    this.Products.Remove(removedProduct);
                    break;
            }
        }
    }
}
