using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.EventStoreAccessors
{

    /*
     * These services should be accessed from event handlers to persist events and/or rehydrate
     * aggregates before dispatching events
     * **/
    public class EventRepository : IEventRepository
    {
        AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Dispatch event, should be called from event handlers. 
        /// Take AggregateRoot object for payload and aggregateId. 
        /// </summary>
        public async Task PersistAsync(BaseEvent @event)
        {
            // append the event to event store
            try
            {
                var ev = new EventStore
                {
                    Id = @event.EventId,
                    AggregateId = @event.AggregateId,
                    Payload = @event.Payload,
                    Timestamp = DateTime.Now,
                    Version = @event.Version
                };

                await _context.EventStore.AddAsync(ev);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Read events from store for a particular aggregate to get its current state. 
        /// Rather than reading the eventually consistent read model it is imperative 
        /// to read the events to get the final state of any aggregate for further operation on it.
        /// </summary>
        /// <param name="aggregateId"></param>
        public void Rehydrate(Guid aggregateId)
        {
            // read events from the first event or from a latest snapshot
            // and regenerate the current state of an aggregate and return the aggregate
        }
    }
}
