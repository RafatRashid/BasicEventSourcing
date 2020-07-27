using MediatR;
using Newtonsoft.Json;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Events
{
    public class ProductQuantityChanged: BaseEvent
    {
        public int ChangedQuantity { get; set; }

        public ProductQuantityChanged(EventStore @event) : base(@event)
        {
            ChangedQuantity = (int)DeserializedPayload.ChangedQuantity;
        }
        public ProductQuantityChanged(AggregateRoot aggregate, int quantity)
        {
            AggregateId = aggregate.AggregateId;
            ChangedQuantity = quantity;
            EventType = typeof(ProductQuantityChanged).Name;
        }
    }


    public class ProductQuantityChangedHandler : INotificationHandler<ProductQuantityChanged>
    {
        IEventRepository _eventRepository;
        IProductRepository _repository;

        // should this handler be allowed to use queries???
        public ProductQuantityChangedHandler(IEventRepository eventRepository, IProductRepository repository)
        {
            _eventRepository = eventRepository;
            _repository = repository;
        }

        public async Task Handle(ProductQuantityChanged @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.ChangedQuantity
            });
            await _eventRepository.PersistAsync(@event);
            _repository.UpdateProductCount(@event.AggregateId, @event.ChangedQuantity);
            await _repository.SaveAsync();
        }
    }
}
