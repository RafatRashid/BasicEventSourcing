using PracticalEventSourcing.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.EventStoreAccessors
{
    public interface IEventRepository
    {
        public Task PersistAsync(BaseEvent @event);
        public Task<T> RehydrateAsync<T>(Guid aggregateId) where T : AggregateRoot, new();
    }
}
