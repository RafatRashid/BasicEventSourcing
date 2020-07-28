using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Domain.Aggregates
{
    public class Product : AggregateRoot
    {
        public string ProductName { get; set; }
        public int AvailableQuatity { get; set; }


        internal void Create(Guid productId, string productName, int availableQuantity)
        {
            var productCreated = new ProductCreated(productId, productName, availableQuantity);
            AddEvent(productCreated);
        }

        internal void ChangeQuantity(int quantity)
        {
            var quantityChanged = new ProductQuantityChanged(this, quantity);
            AddEvent(quantityChanged);
        }


        public override void ApplyEvent(IEvent @event)
        {
            switch(@event)
            {
                case ProductCreated p:
                    this.AggregateId = p.AggregateId;
                    this.ProductName = p.ProductName;
                    this.AvailableQuatity = p.AvailableQuantity;
                    break;

                case ProductQuantityChanged c:
                    this.AvailableQuatity += c.ChangedQuantity;
                    break;
            }
        }

    }
}
