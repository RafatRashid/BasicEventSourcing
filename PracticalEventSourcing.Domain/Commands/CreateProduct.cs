using MediatR;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Commands
{
    public class CreateProduct: BaseCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public CreateProduct(Guid productId, string productName, int quantity)
        {
            ProductId = productId;
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Quantity = quantity;
        }
    }


    public class CreateProductHandler : AsyncRequestHandler<CreateProduct>
    {
        IMediator _mediator;
        public CreateProductHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        protected override async Task Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            // create a new aggregate from command parameters. the aggregate methods will queue necessary events
            var newProduct = new Product();
            newProduct.Create(command.ProductId, command.ProductName, command.Quantity);

            // after preparing events, fire events for persistence and readmodel change
            await newProduct.DispatchEvents(_mediator);
        }
    }
}
