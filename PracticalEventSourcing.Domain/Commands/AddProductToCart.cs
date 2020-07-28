using MediatR;
using PracticalEventSourcing.Core.EventStoreAccessors;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PracticalEventSourcing.Domain.Commands
{
    public class AddProductToCart : BaseCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public AddProductToCart(Guid cartId, Guid productId, int quantity)
        {
            AggregateId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }


    public class AddProductToCartHandler : AsyncRequestHandler<AddProductToCart>
    {
        IEventRepository repository;
        IMediator _mediator;
        public AddProductToCartHandler(IEventRepository repository, IMediator mediator)
        {
            this.repository = repository;
            _mediator = mediator;
        }


        protected override async Task Handle(AddProductToCart command, CancellationToken cancellationToken)
        {
            var cart = await repository.RehydrateAsync<Cart>(command.AggregateId);
            if (cart == null || cart.AggregateId == Guid.Empty)
                throw new Exception("Cart is not available");


            var newProductInCart = await repository.RehydrateAsync<Product>(command.ProductId);

            if (newProductInCart.AvailableQuatity == 0)
                throw new Exception($"Cannot assign product {newProductInCart.ProductName}");

            newProductInCart.ChangeQuantity(newProductInCart.AvailableQuatity-command.Quantity);
            cart.AddProduct(command.ProductId, command.Quantity);

            await cart.DispatchEvents(_mediator);
            await newProductInCart.DispatchEvents(_mediator);
        }
    }
}
