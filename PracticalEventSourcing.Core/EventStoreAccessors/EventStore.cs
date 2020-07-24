using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.EventStoreAccessors
{
    public class EventStore : BaseEntity
    {
        public Guid AggregateId { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
        public int Version { get; set; }
    }
}
