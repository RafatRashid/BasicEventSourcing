using MediatR;
using Newtonsoft.Json;
using PracticalEventSourcing.Core.EventStoreAccessors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Domain.Events
{
    public class BaseEvent : IEvent
    {
        public Guid EventId { get; set; }
        public Guid AggregateId { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public int Version { get; set; }

        public dynamic DeserializedPayload { get; set; }

        public BaseEvent()
        {

        }

        public BaseEvent(EventStore @event)
        {
            EventId = @event.Id;
            AggregateId = @event.AggregateId;
            Version = @event.Version;
            EventType = @event.EventType;

            DeserializedPayload = JsonConvert.DeserializeObject(@event.Payload);
        }
    }
}
