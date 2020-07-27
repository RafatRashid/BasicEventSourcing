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
    public class ChangeProductQuantity : ICommand, IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public ChangeProductQuantity(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }


    internal class ChangeProductQuantityHandler : IRequestHandler<ChangeProductQuantity, bool>
    {
        IEventRepository repository;
        IMediator _mediator;
        public ChangeProductQuantityHandler(IEventRepository repository, IMediator mediator)
        {
            this.repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ChangeProductQuantity request, CancellationToken cancellationToken)
        {
            var product = await repository.RehydrateAsync<Product>(request.ProductId);
            if (product == null || product.AggregateId == Guid.Empty)
                throw new Exception("Product not found");

            product.ChangeQuantity(request.Quantity);
            await product.DispatchEvents(_mediator);
            return true;
        }
    }
}
