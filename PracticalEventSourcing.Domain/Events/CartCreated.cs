using MediatR;
using Newtonsoft.Json;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Events
{
    public class CartCreated : BaseEvent
    {
        public DateTime CreatedAt { get; set; }

        public CartCreated(EventStore @event) : base(@event)
        {
            CreatedAt = (DateTime)DeserializedPayload.CreatedAt;
        }
        public CartCreated(Guid cartId, DateTime createdAt)
        {
            EventId = Guid.NewGuid();
            EventType = GetType().Name;
            AggregateId = cartId;

            CreatedAt = createdAt;
        }
    }


    public class CartCreatedHandler : INotificationHandler<CartCreated>
    {
        IEventRepository _eventRepository;
        ICommandRepository<CartRM> _repository;
        public CartCreatedHandler(IEventRepository eventRepo, ICommandRepository<CartRM> repository)
        {
            _eventRepository = eventRepo;
            _repository = repository;
        }

        public async Task Handle(CartCreated @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.CreatedAt
            });
            await _eventRepository.PersistAsync(@event);

            var cart = new CartRM();
            cart.CreatedAt = @event.CreatedAt;
            await _repository.InsertAsync(cart);
            await _repository.SaveAsync();
        }
    }
}
