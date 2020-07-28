using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.EventStoreAccessors
{
    public abstract class AggregateRoot
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        protected Queue<IEvent> Events = new Queue<IEvent>();

        
        /// <summary>
        /// Clear events from event queue
        /// </summary>
        protected void ClearEvents()
        {
            Events.Clear();
        }

        /// <summary>
        /// Add events to the event queue for processing with respect to current aggregate
        /// </summary>
        /// <param name="event"></param>
        protected void AddEvent(IEvent @event)
        {
            Events.Enqueue(@event);
        }

        /// <summary>
        /// Publish events for handlers to handle
        /// </summary>
        public async Task DispatchEvents(IMediator mediator)
        {
            foreach (var e in Events)
            {
                await mediator.Publish(e);
            }
            ClearEvents();
        }

        /// <summary>
        /// Apply events queued to the event queue to get the state of current aggregate object
        /// Intended to be used for rehydration
        /// Should be overriden by aggregates
        /// </summary>
        public abstract void ApplyEvent(IEvent @event);
    }
}
