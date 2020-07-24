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
    public class ProductQuantityChanged: BaseEvent
    {
        public int ChangedQuantity { get; set; }

        public ProductQuantityChanged(EventStore @event)
        {
            EventId = @event.Id;
            AggregateId = @event.AggregateId;
            Version = @event.Version;
            EventType = @event.EventType;

            dynamic payload = JsonConvert.DeserializeObject(@event.Payload);
            ChangedQuantity = (int)payload.ChangedQuantity;
        }
        public ProductQuantityChanged(Guid aggregateId, int quantity)
        {
            AggregateId = aggregateId;
            ChangedQuantity = quantity;
            EventType = typeof(ProductQuantityChanged).Name;
        }
    }


    public class ProductQuantityChangedHandler : INotificationHandler<ProductQuantityChanged>
    {
        IEventRepository _eventRepository;
        ICommandRepository<ProductRM> _repository;
        private readonly IQueryRepository<ProductRM> queryRepository;

        // should this handler be allowed to use queries???
        public ProductQuantityChangedHandler(IEventRepository eventRepository, ICommandRepository<ProductRM> repository, IQueryRepository<ProductRM> queryRepository)
        {
            _eventRepository = eventRepository;
            _repository = repository;
            this.queryRepository = queryRepository;
        }

        public async Task Handle(ProductQuantityChanged @event, CancellationToken cancellationToken)
        {
            @event.Payload = JsonConvert.SerializeObject(new
            {
                @event.ChangedQuantity
            });
            await _eventRepository.PersistAsync(@event);

            var existingProduct = await queryRepository.GetAsync(@event.AggregateId);
            existingProduct.AvailableQuantity = @event.ChangedQuantity;

            await _repository.UpdateAsync(existingProduct);
        }
    }
}
