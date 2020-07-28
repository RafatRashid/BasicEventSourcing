using MediatR;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Commands
{
    public class AddProductQuantity : BaseCommand
    {
        public Guid ProductId { get; set; }
        public int AddedQuantity { get; set; }

        public AddProductQuantity(Guid productId, int addedQuantity)
        {
            ProductId = productId;
            AddedQuantity = addedQuantity;
        }
    }


    internal class AddProductQuantityHandler : AsyncRequestHandler<AddProductQuantity>
    {
        IEventRepository repository;
        IMediator _mediator;
        public AddProductQuantityHandler(IEventRepository repository, IMediator mediator)
        {
            this.repository = repository;
            _mediator = mediator;
        }

        protected override async Task Handle(AddProductQuantity request, CancellationToken cancellationToken)
        {
            var product = await repository.RehydrateAsync<Product>(request.ProductId);
            if (product == null || product.AggregateId == Guid.Empty)
                throw new Exception("Product not found");

            product.AddQuantity(request.AddedQuantity);
            await product.DispatchEvents(_mediator);
        }
    }
}
