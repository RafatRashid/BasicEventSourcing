using MediatR;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Events
{
    public class ProductCreated : BaseEvent
    {
        public string ProductName { get; set; }
        public int AvailableQuantity { get; set; }


        public ProductCreated(Guid productId, string productName, int availableQuantity)
        {
            EventId = Guid.NewGuid();
            AggregateId = productId;
            ProductName = productName;
            AvailableQuantity = availableQuantity;
        }


        internal class ProductCreatedHandler : INotificationHandler<ProductCreated>
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
                    @event.Payload = JsonSerializer.Serialize(new
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
                    _repository.InsertAsync(newProduct);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
