using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Commands
{
    public class RemoveProductFromCart : BaseCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public RemoveProductFromCart(Guid cartId, Guid productId, int quantity)
        {
            AggregateId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }


    public class RemoveProductFromCartHandler : AsyncRequestHandler<RemoveProductFromCart>
    {
        IEventRepository repository;
        IMediator _mediator;
        public RemoveProductFromCartHandler(IEventRepository repository, IMediator mediator)
        {
            this.repository = repository;
            _mediator = mediator;
        }


        protected override async Task Handle(RemoveProductFromCart command, CancellationToken cancellationToken)
        {
            var cart = await repository.RehydrateAsync<Cart>(command.AggregateId);
            if (cart == null || cart.AggregateId == Guid.Empty)
                throw new Exception("Cart is not available");


            if (!cart.Products.Any(x => x.AggregateId.Equals(command.ProductId)))
                throw new Exception($"Product missing from cart");

            cart.RemoveProduct(command.ProductId, command.Quantity);

            var removedProduct = await repository.RehydrateAsync<Product>(command.ProductId);
            removedProduct.AddQuantity(command.Quantity);

            await cart.DispatchEvents(_mediator);
            await removedProduct.DispatchEvents(_mediator);
        }
    }
}
