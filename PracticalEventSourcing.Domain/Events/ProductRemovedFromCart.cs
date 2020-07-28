using MediatR;
using Newtonsoft.Json;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Events
{
    public class ProductRemovedFromCart : BaseEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }


        public ProductRemovedFromCart(EventStore @event) : base(@event)
        {
            ProductId = (Guid)DeserializedPayload.ProductId;
            Quantity = (int)DeserializedPayload.Quantity;
        }
        public ProductRemovedFromCart(Guid cartId, Guid productId, int quantity)
        {
            AggregateId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }


    public class ProductRemovedFromCartHandler : INotificationHandler<ProductRemovedFromCart>
    {
        IEventRepository _eventRepository;
        ICartRepository _cartRepository;
        IProductRepository _productRepository;
        public ProductRemovedFromCartHandler(
            IEventRepository eventRepo, 
            ICartRepository cartRepository, 
            IProductRepository productRepository
        )
        {
            _eventRepository = eventRepo;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }


        public async Task Handle(ProductRemovedFromCart @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.ProductId,
                Quantity = 1
            });

            _cartRepository.DeleteCartItem(@event.AggregateId, @event.ProductId);
            await _eventRepository.PersistAsync(@event);
            await _eventRepository.SaveAsync();
        }
    }
}
