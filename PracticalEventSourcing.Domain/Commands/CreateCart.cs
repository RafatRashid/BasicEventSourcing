using MediatR;
using PracticalEventSourcing.Domain.Aggregates;
using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Commands
{
    public class CreateCart : BaseCommand, IRequest
    {
        public DateTime CreatedAt { get; set; }

        public CreateCart(Guid cartId, DateTime createdAt)
        {
            AggregateId = cartId;
            CreatedAt = createdAt;
        }
    }


    public class CreateCartHandler : AsyncRequestHandler<CreateCart>
    {
        IMediator _mediator;
        public CreateCartHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        protected override async Task Handle(CreateCart command, CancellationToken cancellationToken)
        {
            var cart = new Cart();
            cart.CreateCart(command.AggregateId, command.CreatedAt);

            await cart.DispatchEvents(_mediator);
        }
    }
}
