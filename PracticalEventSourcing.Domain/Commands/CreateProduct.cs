using MediatR;
using PracticalEventSourcing.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Commands
{
    public class CreateProduct:IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public CreateProduct(Guid productId, string productName)
        {
            ProductId = productId;
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        }
    }


    public class CreateProductHandler : ICommand, IRequestHandler<CreateProduct, bool>
    {
        IMediator _mediator;
        public CreateProductHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            // create a new aggregate from command parameters. the aggregate methods will queue necessary events
            var newProduct = new Product();
            newProduct.Create(command.ProductId, command.ProductName, 10);

            // after preparing events, fire events for persistence and readmodel change
            await newProduct.DispatchEvents(_mediator);

            return true;
        }
    }
}
