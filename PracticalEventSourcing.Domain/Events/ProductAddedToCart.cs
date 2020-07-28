using MediatR;
using Newtonsoft.Json;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Events
{
    public class ProductAddedToCart : BaseEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }


        public ProductAddedToCart(EventStore @event):base(@event)
        {
            ProductId = (Guid)DeserializedPayload.ProductId;
            Quantity = (int)DeserializedPayload.Quantity;
        }
        public ProductAddedToCart(Guid cartId, Guid productId, int quantity)
        {
            AggregateId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }


    public class ProductAddedToCartHandler : INotificationHandler<ProductAddedToCart>
    {
        IEventRepository _eventRepository;
        ICommandRepository<CartItemRM> _repository;
        public ProductAddedToCartHandler(IEventRepository eventRepo, ICommandRepository<CartItemRM> repository)
        {
            _eventRepository = eventRepo;
            _repository = repository;
        }


        public async Task Handle(ProductAddedToCart @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.ProductId,
                Quantity = @event.Quantity
            });

            await _repository.InsertAsync(new CartItemRM
            {
                CartId = @event.AggregateId,
                ProductId = @event.ProductId,
                Quantity = @event.Quantity
            });
            
            await _eventRepository.PersistAsync(@event);
            await _eventRepository.SaveAsync();
        }
    }
}