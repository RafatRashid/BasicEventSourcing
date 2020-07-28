using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Domain.Commands
{
    public class BaseCommand : IRequest
    {
        public Guid AggregateId { get; set; }
    }
}
