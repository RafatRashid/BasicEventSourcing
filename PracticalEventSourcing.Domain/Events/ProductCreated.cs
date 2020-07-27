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
    public class ProductCreated : BaseEvent
    {
        public string ProductName { get; set; }
        public int AvailableQuantity { get; set; }


        public ProductCreated(EventStore @event)
        {
            EventId = @event.Id;
            AggregateId = @event.AggregateId;
            Version = @event.Version;
            EventType = @event.EventType;

            dynamic payload = JsonConvert.DeserializeObject(@event.Payload);
            ProductName = (string)payload.ProductName;
            AvailableQuantity = (int)payload.AvailableQuantity;
        }
        public ProductCreated(Guid productId, string productName, int availableQuantity)
        {
            EventId = Guid.NewGuid();
            EventType = typeof(ProductCreated).Name;
            AggregateId = productId;
            ProductName = productName;
            AvailableQuantity = availableQuantity;
        }

    }


    public class ProductCreatedHandler : INotificationHandler<ProductCreated>
    {
        IEventRepository _eventRepository;
        ICommandRepository<ProductRM> _repository;
        public ProductCreatedHandler(IEventRepository eventRepo, ICommandRepository<ProductRM> repository)
        {
            _eventRepository = eventRepo;
            _repository = repository;
        }

        public async Task Handle(ProductCreated @event, CancellationToken cancellationToken)
        {
            try
            {
                @event.Payload = JsonConvert.SerializeObject(new
                {
                    @event.ProductName,
                    @event.AvailableQuantity
                });
                await _eventRepository.PersistAsync(@event);

                var newProduct = new ProductRM()
                {
                    Id = @event.AggregateId,
                    Name = @event.ProductName,
                    AvailableQuantity = @event.AvailableQuantity
                };
                await _repository.InsertAsync(newProduct);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
