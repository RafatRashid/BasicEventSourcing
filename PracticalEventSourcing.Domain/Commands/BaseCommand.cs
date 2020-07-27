using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Domain.Commands
{
    public class BaseCommand
    {
        public Guid AggregateId { get; set; }
    }
}
