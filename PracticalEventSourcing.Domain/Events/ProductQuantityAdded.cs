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
    public class ProductQuantityAdded: BaseEvent
    {
        public int AddedQuantity { get; set; }

        public ProductQuantityAdded(EventStore @event) : base(@event)
        {
            AddedQuantity = (int)DeserializedPayload.AddedQuantity;
        }
        public ProductQuantityAdded(AggregateRoot aggregate, int addedQuantity)
        {
            AggregateId = aggregate.AggregateId;
            AddedQuantity = addedQuantity;
            EventType = typeof(ProductQuantityAdded).Name;
        }
    }


    public class ProductQuantityAddedHandler : INotificationHandler<ProductQuantityAdded>
    {
        IEventRepository _eventRepository;
        IProductRepository _repository;

        // should this handler be allowed to use queries???
        public ProductQuantityAddedHandler(IEventRepository eventRepository, IProductRepository repository)
        {
            _eventRepository = eventRepository;
            _repository = repository;
        }

        public async Task Handle(ProductQuantityAdded @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.AddedQuantity
            });
            await _eventRepository.PersistAsync(@event);
            _repository.UpdateProductCount(@event.AggregateId, @event.AddedQuantity);
            await _repository.SaveAsync();
        }
    }
}
